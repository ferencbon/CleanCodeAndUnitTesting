using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace week001_homework
{
    public class Calculator
    {
        /// <summary>
        /// Adds two double precision floating point numbers.
        /// </summary>
        /// <param name="a">The first number to add.</param>
        /// <param name="b">The second number to add.</param>
        /// <returns>The sum of the two numbers.</returns>
        public double Add(double  a, double b)
        {
            return a + b;
        }
        
        /// <summary>
        /// Subtracts the second double precision floating point number from the first.
        /// </summary>
        /// <param name="a">The number from which to subtract.</param>
        /// <param name="b">The number to subtract.</param>
        /// <returns>The difference of the two numbers.</returns>
        public double Subtract(double a, double b)
        {
            return a - b;
        }
        
        /// <summary>
        /// Multiplies two double precision floating point numbers.
        /// </summary>
        /// <param name="a">The first number to multiply.</param>
        /// <param name="b">The second number to multiply.</param>
        /// <returns>The product of the two numbers.</returns>
        public double Multiply(double a, double b)
        {
            return a * b;
        }

        /// <summary>
        /// Divides the first double precision floating point number by the second.
        /// </summary>
        /// <param name="a">The dividend.</param>
        /// <param name="b">The divisor.</param>
        /// <returns>The quotient of the two numbers.</returns>
        /// <exception cref="System.DivideByZeroException">Thrown when the divisor is zero.</exception>
        public double Divide(double a, double b)
        {
            if (b == 0)
                throw new DivideByZeroException();
            return a / b;
        }

        /// <summary>
        /// Calculates the square root of a double precision floating point number.
        /// </summary>
        /// <param name="a">The number to calculate the square root of.</param>
        /// <returns>The square root of the number.</returns>
        /// <exception cref="System.Exception">Thrown when the input number is negative.</exception>
        public double Sqrt(double a)
        {
            if (a < 0)
            {
                throw new Exception("Square root of a negative number is not allowed.");
            }
            return Math.Sqrt(a);
        }
        /// <summary>
        /// Raises a double precision floating point number to the power of another.
        /// </summary>
        /// <param name="a">The base number.</param>
        /// <param name="b">The exponent.</param>
        /// <returns>The result of raising the base to the power of the exponent.</returns>
        public double Power(double a, double b)
        {
            return Math.Pow(a, b);
        }
    }

}
