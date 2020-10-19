using System;
using FluentAssertions;
using NSubstitute;
using NSubstitute.ExceptionExtensions;
using NSubstitute.Routing.Handlers;
using NUnit.Framework;
using SocialNetwork;


namespace Tests {
    public class Tests
    {
        [Test]
        public void sum_in_case_of_0_arguments()
        {
            string input = "";

            int number = Calculator.Add(input);

            number.Should().Be(0);
        }

        [Test]
        public void sum_in_case_of_1_arguments()
        {
            string input = "1";

            int number = Calculator.Add(input);

            number.Should().Be(1);
        }

        public void sum_in_case_of_2_arguments()
        {
            string input = "1,2";

            int number = Calculator.Add(input);

            number.Should().Be(3);
        }

        [Test]
        public void sum_in_case_of_unknown_argument()
        {
            string input = "1,2,3";

            int number = Calculator.Add(input);

            number.Should().Be(6);
        }

        [Test]
        public void sum_in_case_of_have_a_breakline_delimiter()
        {
            string input = "1\n2";

            int number = Calculator.Add(input);

            number.Should().Be(3);
        }

        [Test]
        public void sum_in_case_of_different_delimiters()
        {
            string input = "//;\n1;2";

            int number = Calculator.Add(input); 

            number.Should().Be(3);
        }

        [Test]
        public void sum_in_case_of_negative_number()
        {
            string input = "1,-2,4,-5";

            Action exception = () => Calculator.Add(input);

            exception.Should().Throw<System.Exception>().WithMessage("negative numbers not allowed: -2,-5");
        }

        [Test]
        public void not_sum_in_case_of_a_big_number()
        {
            string input = "1,1001,4";

            int number = Calculator.Add(input);

            number.Should().Be(5);
        }
    }
}