// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using System.Globalization;
using System.Runtime.InteropServices;
using Xunit;

namespace System.Numerics.Tests
{
    public class Vector2Tests
    {
        [Fact]
        public void Vector2MarshalSizeTest()
        {
            Assert.Equal(8*2, Marshal.SizeOf<Vector2>());
            Assert.Equal(8*2, Marshal.SizeOf<Vector2>(new Vector2()));
        }

        [Fact]
        public void Vector2CopyToTest()
        {
            Vector2 v1 = new Vector2(2.0, 3.0);

            double[] a = new double[3];
            double[] b = new double[2];

            Assert.Throws<NullReferenceException>(() => v1.CopyTo(null, 0));
            Assert.Throws<ArgumentOutOfRangeException>(() => v1.CopyTo(a, -1));
            Assert.Throws<ArgumentOutOfRangeException>(() => v1.CopyTo(a, a.Length));
            Assert.Throws<ArgumentException>(() => v1.CopyTo(a, 2));

            v1.CopyTo(a, 1);
            v1.CopyTo(b);
            Assert.Equal(0.0, a[0]);
            Assert.Equal(2.0, a[1]);
            Assert.Equal(3.0, a[2]);
            Assert.Equal(2.0, b[0]);
            Assert.Equal(3.0, b[1]);
        }

        [Fact]
        public void Vector2GetHashCodeTest()
        {
            Vector2 v1 = new Vector2(2.0, 3.0);
            Vector2 v2 = new Vector2(2.0, 3.0);
            Vector2 v3 = new Vector2(3.0, 2.0);
            Assert.Equal(v1.GetHashCode(), v1.GetHashCode());
            Assert.Equal(v1.GetHashCode(), v2.GetHashCode());
            Assert.NotEqual(v1.GetHashCode(), v3.GetHashCode());
            Vector2 v4 = new Vector2(0.0, 0.0);
            Vector2 v6 = new Vector2(1.0, 0.0);
            Vector2 v7 = new Vector2(0.0, 1.0);
            Vector2 v8 = new Vector2(1.0, 1.0);
            Assert.NotEqual(v4.GetHashCode(), v6.GetHashCode());
            Assert.NotEqual(v4.GetHashCode(), v7.GetHashCode());
            Assert.NotEqual(v4.GetHashCode(), v8.GetHashCode());
            Assert.NotEqual(v7.GetHashCode(), v6.GetHashCode());
            Assert.NotEqual(v8.GetHashCode(), v6.GetHashCode());
            Assert.NotEqual(v8.GetHashCode(), v7.GetHashCode());
        }

        [Fact]
        public void Vector2ToStringTest()
        {
            string separator = CultureInfo.CurrentCulture.NumberFormat.NumberGroupSeparator;
            CultureInfo enUsCultureInfo = new CultureInfo("en-US");

            Vector2 v1 = new Vector2(2.0, 3.0);

            string v1str = v1.ToString();
            string expectedv1 = string.Format(CultureInfo.CurrentCulture
                , "<{1:G}{0} {2:G}>"
                , new object[] { separator, 2, 3 });
            Assert.Equal(expectedv1, v1str);

            string v1strformatted = v1.ToString("c", CultureInfo.CurrentCulture);
            string expectedv1formatted = string.Format(CultureInfo.CurrentCulture
                , "<{1:c}{0} {2:c}>"
                , new object[] { separator, 2, 3 });
            Assert.Equal(expectedv1formatted, v1strformatted);

            string v2strformatted = v1.ToString("c", enUsCultureInfo);
            string expectedv2formatted = string.Format(enUsCultureInfo
                , "<{1:c}{0} {2:c}>"
                , new object[] { enUsCultureInfo.NumberFormat.NumberGroupSeparator, 2, 3 });
            Assert.Equal(expectedv2formatted, v2strformatted);

            string v3strformatted = v1.ToString("c");
            string expectedv3formatted = string.Format(CultureInfo.CurrentCulture
                , "<{1:c}{0} {2:c}>"
                , new object[] { separator, 2, 3 });
            Assert.Equal(expectedv3formatted, v3strformatted);
        }

        // A test for Distance (Vector2, Vector2)
        [Fact]
        public void Vector2DistanceTest()
        {
            Vector2 a = new Vector2(1.0, 2.0);
            Vector2 b = new Vector2(3.0, 4.0);

            double expected = (double)System.Math.Sqrt(8);
            double actual;

            actual = Vector2.Distance(a, b);
            Assert.True(MathHelper.Equal(expected, actual), "Vector2.Distance did not return the expected value.");
        }

        // A test for Distance (Vector2, Vector2)
        // Distance from the same point
        [Fact]
        public void Vector2DistanceTest2()
        {
            Vector2 a = new Vector2(1.051, 2.05);
            Vector2 b = new Vector2(1.051, 2.05);

            double actual = Vector2.Distance(a, b);
            Assert.Equal(0.0, actual);
        }

        // A test for DistanceSquared (Vector2, Vector2)
        [Fact]
        public void Vector2DistanceSquaredTest()
        {
            Vector2 a = new Vector2(1.0, 2.0);
            Vector2 b = new Vector2(3.0, 4.0);

            double expected = 8.0;
            double actual;

            actual = Vector2.DistanceSquared(a, b);
            Assert.True(MathHelper.Equal(expected, actual), "Vector2.DistanceSquared did not return the expected value.");
        }

        // A test for Dot (Vector2, Vector2)
        [Fact]
        public void Vector2DotTest()
        {
            Vector2 a = new Vector2(1.0, 2.0);
            Vector2 b = new Vector2(3.0, 4.0);

            double expected = 11.0;
            double actual;

            actual = Vector2.Dot(a, b);
            Assert.True(MathHelper.Equal(expected, actual), "Vector2.Dot did not return the expected value.");
        }

        // A test for Dot (Vector2, Vector2)
        // Dot test for perpendicular vector
        [Fact]
        public void Vector2DotTest1()
        {
            Vector2 a = new Vector2(1.55, 1.55);
            Vector2 b = new Vector2(-1.55, 1.55);

            double expected = 0.0;
            double actual = Vector2.Dot(a, b);
            Assert.Equal(expected, actual);
        }

        // A test for Dot (Vector2, Vector2)
        // Dot test with specail double values
        [Fact]
        public void Vector2DotTest2()
        {
            Vector2 a = new Vector2(double.MinValue, double.MinValue);
            Vector2 b = new Vector2(double.MaxValue, double.MaxValue);

            double actual = Vector2.Dot(a, b);
            Assert.True(double.IsNegativeInfinity(actual), "Vector2.Dot did not return the expected value.");
        }

        // A test for Length ()
        [Fact]
        public void Vector2LengthTest()
        {
            Vector2 a = new Vector2(2.0, 4.0);

            Vector2 target = a;

            double expected = (double)System.Math.Sqrt(20);
            double actual;

            actual = target.Length();

            Assert.True(MathHelper.Equal(expected, actual), "Vector2.Length did not return the expected value.");
        }

        // A test for Length ()
        // Length test where length is zero
        [Fact]
        public void Vector2LengthTest1()
        {
            Vector2 target = new Vector2();
            target.X = 0.0;
            target.Y = 0.0;

            double expected = 0.0;
            double actual;

            actual = target.Length();

            Assert.True(MathHelper.Equal(expected, actual), "Vector2.Length did not return the expected value.");
        }

        // A test for LengthSquared ()
        [Fact]
        public void Vector2LengthSquaredTest()
        {
            Vector2 a = new Vector2(2.0, 4.0);

            Vector2 target = a;

            double expected = 20.0;
            double actual;

            actual = target.LengthSquared();

            Assert.True(MathHelper.Equal(expected, actual), "Vector2.LengthSquared did not return the expected value.");
        }

        // A test for LengthSquared ()
        // LengthSquared test where the result is zero
        [Fact]
        public void Vector2LengthSquaredTest1()
        {
            Vector2 a = new Vector2(0.0, 0.0);

            double expected = 0.0;
            double actual = a.LengthSquared();

            Assert.Equal(expected, actual);
        }

        // A test for Min (Vector2, Vector2)
        [Fact]
        public void Vector2MinTest()
        {
            Vector2 a = new Vector2(-1.0, 4.0);
            Vector2 b = new Vector2(2.0, 1.0);

            Vector2 expected = new Vector2(-1.0, 1.0);
            Vector2 actual;
            actual = Vector2.Min(a, b);
            Assert.True(MathHelper.Equal(expected, actual), "Vector2.Min did not return the expected value.");
        }

        [Fact]
        public void Vector2MinMaxCodeCoverageTest()
        {
            Vector2 min = new Vector2(0, 0);
            Vector2 max = new Vector2(1, 1);
            Vector2 actual;

            // Min.
            actual = Vector2.Min(min, max);
            Assert.Equal(actual, min);

            actual = Vector2.Min(max, min);
            Assert.Equal(actual, min);

            // Max.
            actual = Vector2.Max(min, max);
            Assert.Equal(actual, max);

            actual = Vector2.Max(max, min);
            Assert.Equal(actual, max);
        }

        // A test for Max (Vector2, Vector2)
        [Fact]
        public void Vector2MaxTest()
        {
            Vector2 a = new Vector2(-1.0, 4.0);
            Vector2 b = new Vector2(2.0, 1.0);

            Vector2 expected = new Vector2(2.0, 4.0);
            Vector2 actual;
            actual = Vector2.Max(a, b);
            Assert.True(MathHelper.Equal(expected, actual), "Vector2.Max did not return the expected value.");
        }

        // A test for Clamp (Vector2, Vector2, Vector2)
        [Fact]
        public void Vector2ClampTest()
        {
            Vector2 a = new Vector2(0.5, 0.3);
            Vector2 min = new Vector2(0.0, 0.1);
            Vector2 max = new Vector2(1.0, 1.1);

            // Normal case.
            // Case N1: specified value is in the range.
            Vector2 expected = new Vector2(0.5, 0.3);
            Vector2 actual = Vector2.Clamp(a, min, max);
            Assert.True(MathHelper.Equal(expected, actual), "Vector2.Clamp did not return the expected value.");
            // Normal case.
            // Case N2: specified value is bigger than max value.
            a = new Vector2(2.0, 3.0);
            expected = max;
            actual = Vector2.Clamp(a, min, max);
            Assert.True(MathHelper.Equal(expected, actual), "Vector2.Clamp did not return the expected value.");
            // Case N3: specified value is smaller than max value.
            a = new Vector2(-1.0, -2.0);
            expected = min;
            actual = Vector2.Clamp(a, min, max);
            Assert.True(MathHelper.Equal(expected, actual), "Vector2.Clamp did not return the expected value.");
            // Case N4: combination case.
            a = new Vector2(-2.0, 4.0);
            expected = new Vector2(min.X, max.Y);
            actual = Vector2.Clamp(a, min, max);
            Assert.True(MathHelper.Equal(expected, actual), "Vector2.Clamp did not return the expected value.");
            // User specified min value is bigger than max value.
            max = new Vector2(0.0, 0.1);
            min = new Vector2(1.0, 1.1);

            // Case W1: specified value is in the range.
            a = new Vector2(0.5, 0.3);
            expected = min;
            actual = Vector2.Clamp(a, min, max);
            Assert.True(MathHelper.Equal(expected, actual), "Vector2.Clamp did not return the expected value.");

            // Normal case.
            // Case W2: specified value is bigger than max and min value.
            a = new Vector2(2.0, 3.0);
            expected = min;
            actual = Vector2.Clamp(a, min, max);
            Assert.True(MathHelper.Equal(expected, actual), "Vector2.Clamp did not return the expected value.");

            // Case W3: specified value is smaller than min and max value.
            a = new Vector2(-1.0, -2.0);
            expected = min;
            actual = Vector2.Clamp(a, min, max);
            Assert.True(MathHelper.Equal(expected, actual), "Vector2.Clamp did not return the expected value.");
        }

        // A test for Lerp (Vector2, Vector2, double)
        [Fact]
        public void Vector2LerpTest()
        {
            Vector2 a = new Vector2(1.0, 2.0);
            Vector2 b = new Vector2(3.0, 4.0);

            double t = 0.5;

            Vector2 expected = new Vector2(2.0, 3.0);
            Vector2 actual;
            actual = Vector2.Lerp(a, b, t);
            Assert.True(MathHelper.Equal(expected, actual), "Vector2.Lerp did not return the expected value.");
        }

        // A test for Lerp (Vector2, Vector2, double)
        // Lerp test with factor zero
        [Fact]
        public void Vector2LerpTest1()
        {
            Vector2 a = new Vector2(0.0, 0.0);
            Vector2 b = new Vector2(3.18, 4.25);

            double t = 0.0;
            Vector2 expected = Vector2.Zero;
            Vector2 actual = Vector2.Lerp(a, b, t);
            Assert.True(MathHelper.Equal(expected, actual), "Vector2.Lerp did not return the expected value.");
        }

        // A test for Lerp (Vector2, Vector2, double)
        // Lerp test with factor one
        [Fact]
        public void Vector2LerpTest2()
        {
            Vector2 a = new Vector2(0.0, 0.0);
            Vector2 b = new Vector2(3.18, 4.25);

            double t = 1.0;
            Vector2 expected = new Vector2(3.18, 4.25);
            Vector2 actual = Vector2.Lerp(a, b, t);
            Assert.True(MathHelper.Equal(expected, actual), "Vector2.Lerp did not return the expected value.");
        }

        // A test for Lerp (Vector2, Vector2, double)
        // Lerp test with factor > 1
        [Fact]
        public void Vector2LerpTest3()
        {
            Vector2 a = new Vector2(0.0, 0.0);
            Vector2 b = new Vector2(3.18, 4.25);

            double t = 2.0;
            Vector2 expected = b * 2.0;
            Vector2 actual = Vector2.Lerp(a, b, t);
            Assert.True(MathHelper.Equal(expected, actual), "Vector2.Lerp did not return the expected value.");
        }

        // A test for Lerp (Vector2, Vector2, double)
        // Lerp test with factor < 0
        [Fact]
        public void Vector2LerpTest4()
        {
            Vector2 a = new Vector2(0.0, 0.0);
            Vector2 b = new Vector2(3.18, 4.25);

            double t = -2.0;
            Vector2 expected = -(b * 2.0);
            Vector2 actual = Vector2.Lerp(a, b, t);
            Assert.True(MathHelper.Equal(expected, actual), "Vector2.Lerp did not return the expected value.");
        }

        // A test for Lerp (Vector2, Vector2, double)
        // Lerp test with special double value
        [Fact]
        public void Vector2LerpTest5()
        {
            Vector2 a = new Vector2(45.67, 90.0);
            Vector2 b = new Vector2(double.PositiveInfinity, double.NegativeInfinity);

            double t = 0.408;
            Vector2 actual = Vector2.Lerp(a, b, t);
            Assert.True(double.IsPositiveInfinity(actual.X), "Vector2.Lerp did not return the expected value.");
            Assert.True(double.IsNegativeInfinity(actual.Y), "Vector2.Lerp did not return the expected value.");
        }

        // A test for Lerp (Vector2, Vector2, double)
        // Lerp test from the same point
        [Fact]
        public void Vector2LerpTest6()
        {
            Vector2 a = new Vector2(1.0, 2.0);
            Vector2 b = new Vector2(1.0, 2.0);

            double t = 0.5;

            Vector2 expected = new Vector2(1.0, 2.0);
            Vector2 actual = Vector2.Lerp(a, b, t);
            Assert.True(MathHelper.Equal(expected, actual), "Vector2.Lerp did not return the expected value.");
        }

        // A test for Transform(Vector2, Matrix4x4)
        [Fact]
        public void Vector2TransformTest()
        {
            Vector2 v = new Vector2(1.0, 2.0);
            Matrix4x4 m =
                Matrix4x4.CreateRotationX(MathHelper.ToRadians(30.0)) *
                Matrix4x4.CreateRotationY(MathHelper.ToRadians(30.0)) *
                Matrix4x4.CreateRotationZ(MathHelper.ToRadians(30.0));
            m.M41 = 10.0;
            m.M42 = 20.0;
            m.M43 = 30.0;

            Vector2 expected = new Vector2(10.316987, 22.183012);
            Vector2 actual;

            actual = Vector2.Transform(v, m);
            Assert.True(MathHelper.Equal(expected, actual), "Vector2.Transform did not return the expected value.");
        }

        // A test for Transform(Vector2, Matrix3x2)
        [Fact]
        public void Vector2Transform3x2Test()
        {
            Vector2 v = new Vector2(1.0, 2.0);
            Matrix3x2 m = Matrix3x2.CreateRotation(MathHelper.ToRadians(30.0));
            m.M31 = 10.0;
            m.M32 = 20.0;

            Vector2 expected = new Vector2(9.866025, 22.23205);
            Vector2 actual;

            actual = Vector2.Transform(v, m);
            Assert.True(MathHelper.Equal(expected, actual), "Vector2.Transform did not return the expected value.");
        }

        // A test for TransformNormal (Vector2, Matrix4x4)
        [Fact]
        public void Vector2TransformNormalTest()
        {
            Vector2 v = new Vector2(1.0, 2.0);
            Matrix4x4 m =
                Matrix4x4.CreateRotationX(MathHelper.ToRadians(30.0)) *
                Matrix4x4.CreateRotationY(MathHelper.ToRadians(30.0)) *
                Matrix4x4.CreateRotationZ(MathHelper.ToRadians(30.0));
            m.M41 = 10.0;
            m.M42 = 20.0;
            m.M43 = 30.0;

            Vector2 expected = new Vector2(0.31698729810778076, 2.1830127018922196);
            Vector2 actual;

            actual = Vector2.TransformNormal(v, m);
            Assert.Equal(expected, actual);
        }

        // A test for TransformNormal (Vector2, Matrix3x2)
        [Fact]
        public void Vector2TransformNormal3x2Test()
        {
            Vector2 v = new Vector2(1.0, 2.0);
            Matrix3x2 m = Matrix3x2.CreateRotation(MathHelper.ToRadians(30.0));
            m.M31 = 10.0;
            m.M32 = 20.0;

            Vector2 expected = new Vector2(-0.13397459621556118, 2.2320508075688772);
            Vector2 actual;

            actual = Vector2.TransformNormal(v, m);
            Assert.Equal(expected, actual);
        }

        // A test for Transform (Vector2, Quaternion)
        [Fact]
        public void Vector2TransformByQuaternionTest()
        {
            Vector2 v = new Vector2(1.0, 2.0);

            Matrix4x4 m =
                Matrix4x4.CreateRotationX(MathHelper.ToRadians(30.0)) *
                Matrix4x4.CreateRotationY(MathHelper.ToRadians(30.0)) *
                Matrix4x4.CreateRotationZ(MathHelper.ToRadians(30.0));
            Quaternion q = Quaternion.CreateFromRotationMatrix(m);

            Vector2 expected = Vector2.Transform(v, m);
            Vector2 actual = Vector2.Transform(v, q);
            Assert.True(MathHelper.Equal(expected, actual), "Vector2.Transform did not return the expected value.");
        }

        // A test for Transform (Vector2, Quaternion)
        // Transform Vector2 with zero quaternion
        [Fact]
        public void Vector2TransformByQuaternionTest1()
        {
            Vector2 v = new Vector2(1.0, 2.0);
            Quaternion q = new Quaternion();
            Vector2 expected = v;

            Vector2 actual = Vector2.Transform(v, q);
            Assert.True(MathHelper.Equal(expected, actual), "Vector2.Transform did not return the expected value.");
        }

        // A test for Transform (Vector2, Quaternion)
        // Transform Vector2 with identity quaternion
        [Fact]
        public void Vector2TransformByQuaternionTest2()
        {
            Vector2 v = new Vector2(1.0, 2.0);
            Quaternion q = Quaternion.Identity;
            Vector2 expected = v;

            Vector2 actual = Vector2.Transform(v, q);
            Assert.True(MathHelper.Equal(expected, actual), "Vector2.Transform did not return the expected value.");
        }

        // A test for Normalize (Vector2)
        [Fact]
        public void Vector2NormalizeTest()
        {
            Vector2 a = new Vector2(2.0, 3.0);
            Vector2 expected = new Vector2(0.554700196225229122018341733457, 0.8320502943378436830275126001855);
            Vector2 actual;

            actual = Vector2.Normalize(a);
            Assert.True(MathHelper.Equal(expected, actual), "Vector2.Normalize did not return the expected value.");
        }

        // A test for Normalize (Vector2)
        // Normalize zero length vector
        [Fact]
        public void Vector2NormalizeTest1()
        {
            Vector2 a = new Vector2(); // no parameter, default to 0.0
            Vector2 actual = Vector2.Normalize(a);
            Assert.True(double.IsNaN(actual.X) && double.IsNaN(actual.Y), "Vector2.Normalize did not return the expected value.");
        }

        // A test for Normalize (Vector2)
        // Normalize infinite length vector
        [Fact]
        public void Vector2NormalizeTest2()
        {
            Vector2 a = new Vector2(double.MaxValue, double.MaxValue);
            Vector2 actual = Vector2.Normalize(a);
            Vector2 expected = new Vector2(0, 0);
            Assert.Equal(expected, actual);
        }

        // A test for operator - (Vector2)
        [Fact]
        public void Vector2UnaryNegationTest()
        {
            Vector2 a = new Vector2(1.0, 2.0);

            Vector2 expected = new Vector2(-1.0, -2.0);
            Vector2 actual;

            actual = -a;

            Assert.True(MathHelper.Equal(expected, actual), "Vector2.operator - did not return the expected value.");
        }



        // A test for operator - (Vector2)
        // Negate test with special double value
        [Fact]
        public void Vector2UnaryNegationTest1()
        {
            Vector2 a = new Vector2(double.PositiveInfinity, double.NegativeInfinity);

            Vector2 actual = -a;

            Assert.True(double.IsNegativeInfinity(actual.X), "Vector2.operator - did not return the expected value.");
            Assert.True(double.IsPositiveInfinity(actual.Y), "Vector2.operator - did not return the expected value.");
        }

        // A test for operator - (Vector2)
        // Negate test with special double value
        [Fact]
        public void Vector2UnaryNegationTest2()
        {
            Vector2 a = new Vector2(double.NaN, 0.0);
            Vector2 actual = -a;

            Assert.True(double.IsNaN(actual.X), "Vector2.operator - did not return the expected value.");
            Assert.True(double.Equals(0.0, actual.Y), "Vector2.operator - did not return the expected value.");
        }

        // A test for operator - (Vector2, Vector2)
        [Fact]
        public void Vector2SubtractionTest()
        {
            Vector2 a = new Vector2(1.0, 3.0);
            Vector2 b = new Vector2(2.0, 1.5);

            Vector2 expected = new Vector2(-1.0, 1.5);
            Vector2 actual;

            actual = a - b;

            Assert.True(MathHelper.Equal(expected, actual), "Vector2.operator - did not return the expected value.");
        }

        // A test for operator * (Vector2, double)
        [Fact]
        public void Vector2MultiplyOperatorTest()
        {
            Vector2 a = new Vector2(2.0, 3.0);
            const double factor = 2.0;

            Vector2 expected = new Vector2(4.0, 6.0);
            Vector2 actual;

            actual = a * factor;
            Assert.True(MathHelper.Equal(expected, actual), "Vector2.operator * did not return the expected value.");
        }

        // A test for operator * (double, Vector2)
        [Fact]
        public void Vector2MultiplyOperatorTest2()
        {
            Vector2 a = new Vector2(2.0, 3.0);
            const double factor = 2.0;

            Vector2 expected = new Vector2(4.0, 6.0);
            Vector2 actual;

            actual = factor * a;
            Assert.True(MathHelper.Equal(expected, actual), "Vector2.operator * did not return the expected value.");
        }

        // A test for operator * (Vector2, Vector2)
        [Fact]
        public void Vector2MultiplyOperatorTest3()
        {
            Vector2 a = new Vector2(2.0, 3.0);
            Vector2 b = new Vector2(4.0, 5.0);

            Vector2 expected = new Vector2(8.0, 15.0);
            Vector2 actual;

            actual = a * b;

            Assert.True(MathHelper.Equal(expected, actual), "Vector2.operator * did not return the expected value.");
        }

        // A test for operator / (Vector2, double)
        [Fact]
        public void Vector2DivisionTest()
        {
            Vector2 a = new Vector2(2.0, 3.0);

            double div = 2.0;

            Vector2 expected = new Vector2(1.0, 1.5);
            Vector2 actual;

            actual = a / div;

            Assert.True(MathHelper.Equal(expected, actual), "Vector2.operator / did not return the expected value.");
        }

        // A test for operator / (Vector2, Vector2)
        [Fact]
        public void Vector2DivisionTest1()
        {
            Vector2 a = new Vector2(2.0, 3.0);
            Vector2 b = new Vector2(4.0, 5.0);

            Vector2 expected = new Vector2(2.0 / 4.0, 3.0 / 5.0);
            Vector2 actual;

            actual = a / b;

            Assert.True(MathHelper.Equal(expected, actual), "Vector2.operator / did not return the expected value.");
        }

        // A test for operator / (Vector2, double)
        // Divide by zero
        [Fact]
        public void Vector2DivisionTest2()
        {
            Vector2 a = new Vector2(-2.0, 3.0);

            double div = 0.0;

            Vector2 actual = a / div;

            Assert.True(double.IsNegativeInfinity(actual.X), "Vector2.operator / did not return the expected value.");
            Assert.True(double.IsPositiveInfinity(actual.Y), "Vector2.operator / did not return the expected value.");
        }

        // A test for operator / (Vector2, Vector2)
        // Divide by zero
        [Fact]
        public void Vector2DivisionTest3()
        {
            Vector2 a = new Vector2(0.047, -3.0);
            Vector2 b = new Vector2();

            Vector2 actual = a / b;

            Assert.True(double.IsInfinity(actual.X), "Vector2.operator / did not return the expected value.");
            Assert.True(double.IsInfinity(actual.Y), "Vector2.operator / did not return the expected value.");
        }

        // A test for operator + (Vector2, Vector2)
        [Fact]
        public void Vector2AdditionTest()
        {
            Vector2 a = new Vector2(1.0, 2.0);
            Vector2 b = new Vector2(3.0, 4.0);

            Vector2 expected = new Vector2(4.0, 6.0);
            Vector2 actual;

            actual = a + b;

            Assert.True(MathHelper.Equal(expected, actual), "Vector2.operator + did not return the expected value.");
        }

        // A test for Vector2 (double, double)
        [Fact]
        public void Vector2ConstructorTest()
        {
            double x = 1.0;
            double y = 2.0;

            Vector2 target = new Vector2(x, y);
            Assert.True(MathHelper.Equal(target.X, x) && MathHelper.Equal(target.Y, y), "Vector2(x,y) constructor did not return the expected value.");
        }

        // A test for Vector2 ()
        // Constructor with no parameter
        [Fact]
        public void Vector2ConstructorTest2()
        {
            Vector2 target = new Vector2();
            Assert.Equal(target.X, 0.0);
            Assert.Equal(target.Y, 0.0);
        }

        // A test for Vector2 (double, double)
        // Constructor with special floating values
        [Fact]
        public void Vector2ConstructorTest3()
        {
            Vector2 target = new Vector2(double.NaN, double.MaxValue);
            Assert.Equal(target.X, double.NaN);
            Assert.Equal(target.Y, double.MaxValue);
        }

        // A test for Vector2 (double)
        [Fact]
        public void Vector2ConstructorTest4()
        {
            double value = 1.0;
            Vector2 target = new Vector2(value);

            Vector2 expected = new Vector2(value, value);
            Assert.Equal(expected, target);

            value = 2.0;
            target = new Vector2(value);
            expected = new Vector2(value, value);
            Assert.Equal(expected, target);
        }

        // A test for Add (Vector2, Vector2)
        [Fact]
        public void Vector2AddTest()
        {
            Vector2 a = new Vector2(1.0, 2.0);
            Vector2 b = new Vector2(5.0, 6.0);

            Vector2 expected = new Vector2(6.0, 8.0);
            Vector2 actual;

            actual = Vector2.Add(a, b);
            Assert.Equal(expected, actual);
        }

        // A test for Divide (Vector2, double)
        [Fact]
        public void Vector2DivideTest()
        {
            Vector2 a = new Vector2(1.0, 2.0);
            double div = 2.0;
            Vector2 expected = new Vector2(0.5, 1.0);
            Vector2 actual;
            actual = Vector2.Divide(a, div);
            Assert.Equal(expected, actual);
        }

        // A test for Divide (Vector2, Vector2)
        [Fact]
        public void Vector2DivideTest1()
        {
            Vector2 a = new Vector2(1.0, 6.0);
            Vector2 b = new Vector2(5.0, 2.0);

            Vector2 expected = new Vector2(1.0 / 5.0, 6.0 / 2.0);
            Vector2 actual;

            actual = Vector2.Divide(a, b);
            Assert.Equal(expected, actual);
        }

        // A test for Equals (object)
        [Fact]
        public void Vector2EqualsTest()
        {
            Vector2 a = new Vector2(1.0, 2.0);
            Vector2 b = new Vector2(1.0, 2.0);

            // case 1: compare between same values
            object obj = b;

            bool expected = true;
            bool actual = a.Equals(obj);
            Assert.Equal(expected, actual);

            // case 2: compare between different values
            b.X = 10.0;
            obj = b;
            expected = false;
            actual = a.Equals(obj);
            Assert.Equal(expected, actual);

            // case 3: compare between different types.
            obj = new Quaternion();
            expected = false;
            actual = a.Equals(obj);
            Assert.Equal(expected, actual);

            // case 3: compare against null.
            obj = null;
            expected = false;
            actual = a.Equals(obj);
            Assert.Equal(expected, actual);
        }

        // A test for Multiply (Vector2, double)
        [Fact]
        public void Vector2MultiplyTest()
        {
            Vector2 a = new Vector2(1.0, 2.0);
            const double factor = 2.0;
            Vector2 expected = new Vector2(2.0, 4.0);
            Vector2 actual = Vector2.Multiply(a, factor);
            Assert.Equal(expected, actual);
        }

        // A test for Multiply (double, Vector2)
        [Fact]
        public void Vector2MultiplyTest2()
        {
            Vector2 a = new Vector2(1.0, 2.0);
            const double factor = 2.0;
            Vector2 expected = new Vector2(2.0, 4.0);
            Vector2 actual = Vector2.Multiply(factor, a);
            Assert.Equal(expected, actual);
        }

        // A test for Multiply (Vector2, Vector2)
        [Fact]
        public void Vector2MultiplyTest3()
        {
            Vector2 a = new Vector2(1.0, 2.0);
            Vector2 b = new Vector2(5.0, 6.0);

            Vector2 expected = new Vector2(5.0, 12.0);
            Vector2 actual;

            actual = Vector2.Multiply(a, b);
            Assert.Equal(expected, actual);
        }

        // A test for Negate (Vector2)
        [Fact]
        public void Vector2NegateTest()
        {
            Vector2 a = new Vector2(1.0, 2.0);

            Vector2 expected = new Vector2(-1.0, -2.0);
            Vector2 actual;

            actual = Vector2.Negate(a);
            Assert.Equal(expected, actual);
        }

        // A test for operator != (Vector2, Vector2)
        [Fact]
        public void Vector2InequalityTest()
        {
            Vector2 a = new Vector2(1.0, 2.0);
            Vector2 b = new Vector2(1.0, 2.0);

            // case 1: compare between same values
            bool expected = false;
            bool actual = a != b;
            Assert.Equal(expected, actual);

            // case 2: compare between different values
            b.X = 10.0;
            expected = true;
            actual = a != b;
            Assert.Equal(expected, actual);
        }

        // A test for operator == (Vector2, Vector2)
        [Fact]
        public void Vector2EqualityTest()
        {
            Vector2 a = new Vector2(1.0, 2.0);
            Vector2 b = new Vector2(1.0, 2.0);

            // case 1: compare between same values
            bool expected = true;
            bool actual = a == b;
            Assert.Equal(expected, actual);

            // case 2: compare between different values
            b.X = 10.0;
            expected = false;
            actual = a == b;
            Assert.Equal(expected, actual);
        }

        // A test for Subtract (Vector2, Vector2)
        [Fact]
        public void Vector2SubtractTest()
        {
            Vector2 a = new Vector2(1.0, 6.0);
            Vector2 b = new Vector2(5.0, 2.0);

            Vector2 expected = new Vector2(-4.0, 4.0);
            Vector2 actual;

            actual = Vector2.Subtract(a, b);
            Assert.Equal(expected, actual);
        }

        // A test for UnitX
        [Fact]
        public void Vector2UnitXTest()
        {
            Vector2 val = new Vector2(1.0, 0.0);
            Assert.Equal(val, Vector2.UnitX);
        }

        // A test for UnitY
        [Fact]
        public void Vector2UnitYTest()
        {
            Vector2 val = new Vector2(0.0, 1.0);
            Assert.Equal(val, Vector2.UnitY);
        }

        // A test for One
        [Fact]
        public void Vector2OneTest()
        {
            Vector2 val = new Vector2(1.0, 1.0);
            Assert.Equal(val, Vector2.One);
        }

        // A test for Zero
        [Fact]
        public void Vector2ZeroTest()
        {
            Vector2 val = new Vector2(0.0, 0.0);
            Assert.Equal(val, Vector2.Zero);
        }

        // A test for Equals (Vector2)
        [Fact]
        public void Vector2EqualsTest1()
        {
            Vector2 a = new Vector2(1.0, 2.0);
            Vector2 b = new Vector2(1.0, 2.0);

            // case 1: compare between same values
            bool expected = true;
            bool actual = a.Equals(b);
            Assert.Equal(expected, actual);

            // case 2: compare between different values
            b.X = 10.0;
            expected = false;
            actual = a.Equals(b);
            Assert.Equal(expected, actual);
        }

        // A test for Vector2 comparison involving NaN values
        [Fact]
        public void Vector2EqualsNanTest()
        {
            Vector2 a = new Vector2(double.NaN, 0);
            Vector2 b = new Vector2(0, double.NaN);

            Assert.False(a == Vector2.Zero);
            Assert.False(b == Vector2.Zero);

            Assert.True(a != Vector2.Zero);
            Assert.True(b != Vector2.Zero);

            Assert.False(a.Equals(Vector2.Zero));
            Assert.False(b.Equals(Vector2.Zero));

            // Counterintuitive result - IEEE rules for NaN comparison are weird!
            Assert.False(a.Equals(a));
            Assert.False(b.Equals(b));
        }

        // A test for Reflect (Vector2, Vector2)
        [Fact]
        public void Vector2ReflectTest()
        {
            Vector2 a = Vector2.Normalize(new Vector2(1.0, 1.0));

            // Reflect on XZ plane.
            Vector2 n = new Vector2(0.0, 1.0);
            Vector2 expected = new Vector2(a.X, -a.Y);
            Vector2 actual = Vector2.Reflect(a, n);
            Assert.True(MathHelper.Equal(expected, actual), "Vector2.Reflect did not return the expected value.");

            // Reflect on XY plane.
            n = new Vector2(0.0, 0.0);
            expected = new Vector2(a.X, a.Y);
            actual = Vector2.Reflect(a, n);
            Assert.True(MathHelper.Equal(expected, actual), "Vector2.Reflect did not return the expected value.");

            // Reflect on YZ plane.
            n = new Vector2(1.0, 0.0);
            expected = new Vector2(-a.X, a.Y);
            actual = Vector2.Reflect(a, n);
            Assert.True(MathHelper.Equal(expected, actual), "Vector2.Reflect did not return the expected value.");
        }

        // A test for Reflect (Vector2, Vector2)
        // Reflection when normal and source are the same
        [Fact]
        public void Vector2ReflectTest1()
        {
            Vector2 n = new Vector2(0.45, 1.28);
            n = Vector2.Normalize(n);
            Vector2 a = n;

            Vector2 expected = -n;
            Vector2 actual = Vector2.Reflect(a, n);
            Assert.True(MathHelper.Equal(expected, actual), "Vector2.Reflect did not return the expected value.");
        }

        // A test for Reflect (Vector2, Vector2)
        // Reflection when normal and source are negation
        [Fact]
        public void Vector2ReflectTest2()
        {
            Vector2 n = new Vector2(0.45, 1.28);
            n = Vector2.Normalize(n);
            Vector2 a = -n;

            Vector2 expected = n;
            Vector2 actual = Vector2.Reflect(a, n);
            Assert.True(MathHelper.Equal(expected, actual), "Vector2.Reflect did not return the expected value.");
        }

        [Fact]
        public void Vector2AbsTest()
        {
            Vector2 v1 = new Vector2(-2.5, 2.0);
            Vector2 v3 = Vector2.Abs(new Vector2(0.0, Double.NegativeInfinity));
            Vector2 v = Vector2.Abs(v1);
            Assert.Equal(2.5, v.X);
            Assert.Equal(2.0, v.Y);
            Assert.Equal(0.0, v3.X);
            Assert.Equal(Double.PositiveInfinity, v3.Y);
        }

        [Fact]
        public void Vector2SqrtTest()
        {
            Vector2 v1 = new Vector2(-2.5, 2.0);
            Vector2 v2 = new Vector2(5.5, 4.5);
            Assert.Equal(2, (int)Vector2.SquareRoot(v2).X);
            Assert.Equal(2, (int)Vector2.SquareRoot(v2).Y);
            Assert.Equal(Double.NaN, Vector2.SquareRoot(v1).X);
        }

        // A test to make sure these types are blittable directly into GPU buffer memory layouts
        [Fact]
        public unsafe void Vector2SizeofTest()
        {
            Assert.Equal(8*2, sizeof(Vector2));
            Assert.Equal(16*2, sizeof(Vector2_2x));
            Assert.Equal(12*2, sizeof(Vector2PlusFloat));
            Assert.Equal(24*2, sizeof(Vector2PlusFloat_2x));
        }

        [StructLayout(LayoutKind.Sequential)]
        struct Vector2_2x
        {
            private Vector2 _a;
            private Vector2 _b;
        }

        [StructLayout(LayoutKind.Sequential)]
        struct Vector2PlusFloat
        {
            private Vector2 _v;
            private double _f;
        }

        [StructLayout(LayoutKind.Sequential)]
        struct Vector2PlusFloat_2x
        {
            private Vector2PlusFloat _a;
            private Vector2PlusFloat _b;
        }

        [Fact]
        public void SetFieldsTest()
        {
            Vector2 v3 = new Vector2(4, 5);
            v3.X = 1.0;
            v3.Y = 2.0;
            Assert.Equal(1.0, v3.X);
            Assert.Equal(2.0, v3.Y);
            Vector2 v4 = v3;
            v4.Y = 0.5;
            Assert.Equal(1.0, v4.X);
            Assert.Equal(0.5, v4.Y);
            Assert.Equal(2.0, v3.Y);
        }

        [Fact]
        public void EmbeddedVectorSetFields()
        {
            EmbeddedVectorObject evo = new EmbeddedVectorObject();
            evo.FieldVector.X = 5.0;
            evo.FieldVector.Y = 5.0;
            Assert.Equal(5.0, evo.FieldVector.X);
            Assert.Equal(5.0, evo.FieldVector.Y);
        }

        private class EmbeddedVectorObject
        {
            public Vector2 FieldVector;
        }
    }
}
