using FsCheck;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Linq;
using System.Reflection;
using PropertybasedTesting;
using static Testing.AClass;

namespace Testing.UTest
{
    [TestClass]
    public class UnitTest1
    {
        [TestInitialize]
        public void TestInitialize()
        {
            AnObject = new AClass();
        }

        public AClass AnObject { get; set; }

        [TestMethod]
        public void TestMethod1()
        {
            var b =
                from boolean in new bool[] { true, false }
                select boolean;

            var t =
                from time in
                    Enum
                    .GetValues(typeof(Time))
                    .Cast<Time>()
                select time;

            var u =
                from university in
                    typeof(University)
                    .GetFields(BindingFlags.Static | BindingFlags.Public)
                    .Select(fi => (string)fi.GetValue(null))
                select university;

            var discreateTestPoints =
                from boolean in new bool[] { true, false }
                from time in
                    Enum
                    .GetValues(typeof(Time))
                    .Cast<Time>()
                from university in
                    typeof(University)
                    .GetFields(BindingFlags.Static | BindingFlags.Public)
                    .Select(fi => (string)fi.GetValue(null))
                from level in AnObject.Levels
                select (b: boolean, t: time, u: university, l: level);

            var i =
                Arb
                .From<int>()
                .Filter(integer => integer >= 1 && integer < 100)
                .Generator;

            var j =
                Arb
                .From<int>()
                .Filter(integer => (integer >= 1 && integer <= 50) || (integer >= 60 && integer <= 100))
                .Generator;

            var config = FsCheck.Configuration.QuickThrowOnFailure;
            config.MaxNbOfTest = 1000;

            foreach (var discreateTestPoint in discreateTestPoints)
            {
                Prop
                    .ForAll(
                        Arb.From(i),
                        Arb.From(j),
                        (ii, jj) =>
                        {
                            var result = AnObject.SomeCode(
                                discreateTestPoint.b,
                                discreateTestPoint.t,
                                discreateTestPoint.u,
                                discreateTestPoint.l,
                                ii,
                                jj);

                            Test.Check(
                                when: () => discreateTestPoint.b,
                                then: () => result.d > 10,
                                message: "if b then d > 10");

                            Test.Check(
                                when: () => ii > 50,
                                then: () => result.m == "Tax due",
                                message: "if i > 50 then = 'Tax due'");
                        })
                    .Check(config);
            }
        }
    }
}
