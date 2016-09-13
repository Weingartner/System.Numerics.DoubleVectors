// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using System.Globalization;
using System.Numerics.System.DoubleNumerics;
using System.Runtime.InteropServices;
using Xunit;

namespace System.Numerics.Tests
{
    public class Vector3Tests
    {
        [Fact]
        public void Vector3MarshalSizeTest()
        {
            Assert.Equal(12*2, Marshal.SizeOf<Vector3>());
            Assert.Equal(12*2, Marshal.SizeOf<Vector3>(new Vector3()));
        }

        [Fact]
        public void Vector3CopyToTest()
        {
            Vector3 v1 = new Vector3(2.0, 3.0, 3.3);

            double[] a = new double[4];
            double[] b = new double[3];

            Assert.Throws<NullReferenceException>(() => v1.CopyTo(null, 0));
            Assert.Throws<ArgumentOutOfRangeException>(() => v1.CopyTo(a, -1));
            Assert.Throws<ArgumentOutOfRangeException>(() => v1.CopyTo(a, a.Length));
            Assert.Throws<ArgumentException>(() => v1.CopyTo(a, a.Length - 2));

            v1.CopyTo(a, 1);
            v1.CopyTo(b);
            Assert.Equal(0.0, a[0]);
            Assert.Equal(2.0, a[1]);
            Assert.Equal(3.0, a[2]);
            Assert.Equal(3.3, a[3]);
            Assert.Equal(2.0, b[0]);
            Assert.Equal(3.0, b[1]);
            Assert.Equal(3.3, b[2]);
        }

        [Fact]
        public void Vector3GetHashCodeTest()
        {
            Vector3 v1 = new Vector3(2.0, 3.0, 3.3);
            Vector3 v2 = new Vector3(2.0, 3.0, 3.3);
            Vector3 v3 = new Vector3(2.0, 3.0, 3.3);
            Vector3 v5 = new Vector3(3.0, 2.0, 3.3);
            Assert.Equal(v1.GetHashCode(), v1.GetHashCode());
            Assert.Equal(v1.GetHashCode(), v2.GetHashCode());
            Assert.NotEqual(v1.GetHashCode(), v5.GetHashCode());
            Assert.Equal(v1.GetHashCode(), v3.GetHashCode());
            Vector3 v4 = new Vector3(0.0, 0.0, 0.0);
            Vector3 v6 = new Vector3(1.0, 0.0, 0.0);
            Vector3 v7 = new Vector3(0.0, 1.0, 0.0);
            Vector3 v8 = new Vector3(1.0, 1.0, 1.0);
            Vector3 v9 = new Vector3(1.0, 1.0, 0.0);
            Assert.NotEqual(v4.GetHashCode(), v6.GetHashCode());
            Assert.NotEqual(v4.GetHashCode(), v7.GetHashCode());
            Assert.NotEqual(v4.GetHashCode(), v8.GetHashCode());
            Assert.NotEqual(v7.GetHashCode(), v6.GetHashCode());
            Assert.NotEqual(v8.GetHashCode(), v6.GetHashCode());
            Assert.NotEqual(v8.GetHashCode(), v7.GetHashCode());
            Assert.NotEqual(v8.GetHashCode(), v9.GetHashCode());
            Assert.NotEqual(v7.GetHashCode(), v9.GetHashCode());
        }

        [Fact]
        public void Vector3ToStringTest()
        {
            string separator = CultureInfo.CurrentCulture.NumberFormat.NumberGroupSeparator;
            CultureInfo enUsCultureInfo = new CultureInfo("en-US");

            Vector3 v1 = new Vector3(2.0, 3.0, 3.3);
            string v1str = v1.ToString();
            string expectedv1 = string.Format(CultureInfo.CurrentCulture
                , "<{1:G}{0} {2:G}{0} {3:G}>"
                , separator, 2, 3, 3.3);
            Assert.Equal(expectedv1, v1str);

            string v1strformatted = v1.ToString("c", CultureInfo.CurrentCulture);
            string expectedv1formatted = string.Format(CultureInfo.CurrentCulture
                , "<{1:c}{0} {2:c}{0} {3:c}>"
                , separator, 2, 3, 3.3);
            Assert.Equal(expectedv1formatted, v1strformatted);

            string v2strformatted = v1.ToString("c", enUsCultureInfo);
            string expectedv2formatted = string.Format(enUsCultureInfo
                , "<{1:c}{0} {2:c}{0} {3:c}>"
                , enUsCultureInfo.NumberFormat.NumberGroupSeparator, 2, 3, 3.3);
            Assert.Equal(expectedv2formatted, v2strformatted);

            string v3strformatted = v1.ToString("c");
            string expectedv3formatted = string.Format(CultureInfo.CurrentCulture
                , "<{1:c}{0} {2:c}{0} {3:c}>"
                , separator, 2, 3, 3.3);
            Assert.Equal(expectedv3formatted, v3strformatted);
        }

        // A test for Cross (Vector3, Vector3)
        [Fact]
        public void Vector3CrossTest()
        {
            Vector3 a = new Vector3(1.0, 0.0, 0.0);
            Vector3 b = new Vector3(0.0, 1.0, 0.0);

            Vector3 expected = new Vector3(0.0, 0.0, 1.0);
            Vector3 actual;

            actual = Vector3.Cross(a, b);
            Assert.True(MathHelper.Equal(expected, actual), "Vector3.Cross did not return the expected value.");
        }

        // A test for Cross (Vector3, Vector3)
        // Cross test of the same vector
        [Fact]
        public void Vector3CrossTest1()
        {
            Vector3 a = new Vector3(0.0, 1.0, 0.0);
            Vector3 b = new Vector3(0.0, 1.0, 0.0);

            Vector3 expected = new Vector3(0.0, 0.0, 0.0);
            Vector3 actual = Vector3.Cross(a, b);
            Assert.True(MathHelper.Equal(expected, actual), "Vector3.Cross did not return the expected value.");
        }

        // A test for Distance (Vector3, Vector3)
        [Fact]
        public void Vector3DistanceTest()
        {
            Vector3 a = new Vector3(1.0, 2.0, 3.0);
            Vector3 b = new Vector3(4.0, 5.0, 6.0);

            double expected = (double)Math.Sqrt(27);
            double actual;

            actual = Vector3.Distance(a, b);
            Assert.True(MathHelper.Equal(expected, actual), "Vector3.Distance did not return the expected value.");
        }

        // A test for Distance (Vector3, Vector3)
        // Distance from the same point
        [Fact]
        public void Vector3DistanceTest1()
        {
            Vector3 a = new Vector3(1.051, 2.05, 3.478);
            Vector3 b = new Vector3(new Vector2(1.051, 0.0), 1);
            b.Y = 2.05;
            b.Z = 3.478;

            double actual = Vector3.Distance(a, b);
            Assert.Equal(0.0, actual);
        }

        // A test for DistanceSquared (Vector3, Vector3)
        [Fact]
        public void Vector3DistanceSquaredTest()
        {
            Vector3 a = new Vector3(1.0, 2.0, 3.0);
            Vector3 b = new Vector3(4.0, 5.0, 6.0);

            double expected = 27.0;
            double actual;

            actual = Vector3.DistanceSquared(a, b);
            Assert.True(MathHelper.Equal(expected, actual), "Vector3.DistanceSquared did not return the expected value.");
        }

        // A test for Dot (Vector3, Vector3)
        [Fact]
        public void Vector3DotTest()
        {
            Vector3 a = new Vector3(1.0, 2.0, 3.0);
            Vector3 b = new Vector3(4.0, 5.0, 6.0);

            double expected = 32.0;
            double actual;

            actual = Vector3.Dot(a, b);
            Assert.True(MathHelper.Equal(expected, actual), "Vector3.Dot did not return the expected value.");
        }

        // A test for Dot (Vector3, Vector3)
        // Dot test for perpendicular vector
        [Fact]
        public void Vector3DotTest1()
        {
            Vector3 a = new Vector3(1.55, 1.55, 1);
            Vector3 b = new Vector3(2.5, 3, 1.5);
            Vector3 c = Vector3.Cross(a, b);

            double expected = 0.0;
            double actual1 = Vector3.Dot(a, c);
            double actual2 = Vector3.Dot(b, c);
            Assert.True(MathHelper.Equal(expected, actual1), "Vector3.Dot did not return the expected value.");
            Assert.True(MathHelper.Equal(expected, actual2), "Vector3.Dot did not return the expected value.");
        }

        // A test for Length ()
        [Fact]
        public void Vector3LengthTest()
        {
            Vector2 a = new Vector2(1.0, 2.0);

            double z = 3.0;

            Vector3 target = new Vector3(a, z);

            double expected = (double)Math.Sqrt(14.0);
            double actual;

            actual = target.Length();
            Assert.True(MathHelper.Equal(expected, actual), "Vector3.Length did not return the expected value.");
        }

        // A test for Length ()
        // Length test where length is zero
        [Fact]
        public void Vector3LengthTest1()
        {
            Vector3 target = new Vector3();

            double expected = 0.0;
            double actual = target.Length();
            Assert.True(MathHelper.Equal(expected, actual), "Vector3.Length did not return the expected value.");
        }

        // A test for LengthSquared ()
        [Fact]
        public void Vector3LengthSquaredTest()
        {
            Vector2 a = new Vector2(1.0, 2.0);

            double z = 3.0;

            Vector3 target = new Vector3(a, z);

            double expected = 14.0;
            double actual;

            actual = target.LengthSquared();
            Assert.True(MathHelper.Equal(expected, actual), "Vector3.LengthSquared did not return the expected value.");
        }

        // A test for Min (Vector3, Vector3)
        [Fact]
        public void Vector3MinTest()
        {
            Vector3 a = new Vector3(-1.0, 4.0, -3.0);
            Vector3 b = new Vector3(2.0, 1.0, -1.0);

            Vector3 expected = new Vector3(-1.0, 1.0, -3.0);
            Vector3 actual;
            actual = Vector3.Min(a, b);
            Assert.True(MathHelper.Equal(expected, actual), "Vector3.Min did not return the expected value.");
        }

        // A test for Max (Vector3, Vector3)
        [Fact]
        public void Vector3MaxTest()
        {
            Vector3 a = new Vector3(-1.0, 4.0, -3.0);
            Vector3 b = new Vector3(2.0, 1.0, -1.0);

            Vector3 expected = new Vector3(2.0, 4.0, -1.0);
            Vector3 actual;
            actual = Vector3.Max(a, b);
            Assert.True(MathHelper.Equal(expected, actual), "vector3.Max did not return the expected value.");
        }

        [Fact]
        public void Vector3MinMaxCodeCoverageTest()
        {
            Vector3 min = Vector3.Zero;
            Vector3 max = Vector3.One;
            Vector3 actual;

            // Min.
            actual = Vector3.Min(min, max);
            Assert.Equal(actual, min);

            actual = Vector3.Min(max, min);
            Assert.Equal(actual, min);

            // Max.
            actual = Vector3.Max(min, max);
            Assert.Equal(actual, max);

            actual = Vector3.Max(max, min);
            Assert.Equal(actual, max);
        }

        // A test for Lerp (Vector3, Vector3, double)
        [Fact]
        public void Vector3LerpTest()
        {
            Vector3 a = new Vector3(1.0, 2.0, 3.0);
            Vector3 b = new Vector3(4.0, 5.0, 6.0);

            double t = 0.5;

            Vector3 expected = new Vector3(2.5, 3.5, 4.5);
            Vector3 actual;

            actual = Vector3.Lerp(a, b, t);
            Assert.True(MathHelper.Equal(expected, actual), "Vector3.Lerp did not return the expected value.");
        }

        // A test for Lerp (Vector3, Vector3, double)
        // Lerp test with factor zero
        [Fact]
        public void Vector3LerpTest1()
        {
            Vector3 a = new Vector3(1.0, 2.0, 3.0);
            Vector3 b = new Vector3(4.0, 5.0, 6.0);

            double t = 0.0;
            Vector3 expected = new Vector3(1.0, 2.0, 3.0);
            Vector3 actual = Vector3.Lerp(a, b, t);
            Assert.True(MathHelper.Equal(expected, actual), "Vector3.Lerp did not return the expected value.");
        }

        // A test for Lerp (Vector3, Vector3, double)
        // Lerp test with factor one
        [Fact]
        public void Vector3LerpTest2()
        {
            Vector3 a = new Vector3(1.0, 2.0, 3.0);
            Vector3 b = new Vector3(4.0, 5.0, 6.0);

            double t = 1.0;
            Vector3 expected = new Vector3(4.0, 5.0, 6.0);
            Vector3 actual = Vector3.Lerp(a, b, t);
            Assert.True(MathHelper.Equal(expected, actual), "Vector3.Lerp did not return the expected value.");
        }

        // A test for Lerp (Vector3, Vector3, double)
        // Lerp test with factor > 1
        [Fact]
        public void Vector3LerpTest3()
        {
            Vector3 a = new Vector3(0.0, 0.0, 0.0);
            Vector3 b = new Vector3(4.0, 5.0, 6.0);

            double t = 2.0;
            Vector3 expected = new Vector3(8.0, 10.0, 12.0);
            Vector3 actual = Vector3.Lerp(a, b, t);
            Assert.True(MathHelper.Equal(expected, actual), "Vector3.Lerp did not return the expected value.");
        }

        // A test for Lerp (Vector3, Vector3, double)
        // Lerp test with factor < 0
        [Fact]
        public void Vector3LerpTest4()
        {
            Vector3 a = new Vector3(0.0, 0.0, 0.0);
            Vector3 b = new Vector3(4.0, 5.0, 6.0);

            double t = -2.0;
            Vector3 expected = new Vector3(-8.0, -10.0, -12.0);
            Vector3 actual = Vector3.Lerp(a, b, t);
            Assert.True(MathHelper.Equal(expected, actual), "Vector3.Lerp did not return the expected value.");
        }

        // A test for Lerp (Vector3, Vector3, double)
        // Lerp test from the same point
        [Fact]
        public void Vector3LerpTest5()
        {
            Vector3 a = new Vector3(1.68, 2.34, 5.43);
            Vector3 b = a;

            double t = 0.18;
            Vector3 expected = new Vector3(1.68, 2.34, 5.43);
            Vector3 actual = Vector3.Lerp(a, b, t);
            Assert.True(MathHelper.Equal(expected, actual), "Vector3.Lerp did not return the expected value.");
        }

        // A test for Reflect (Vector3, Vector3)
        [Fact]
        public void Vector3ReflectTest()
        {
            Vector3 a = Vector3.Normalize(new Vector3(1.0, 1.0, 1.0));

            // Reflect on XZ plane.
            Vector3 n = new Vector3(0.0, 1.0, 0.0);
            Vector3 expected = new Vector3(a.X, -a.Y, a.Z);
            Vector3 actual = Vector3.Reflect(a, n);
            Assert.True(MathHelper.Equal(expected, actual), "Vector3.Reflect did not return the expected value.");

            // Reflect on XY plane.
            n = new Vector3(0.0, 0.0, 1.0);
            expected = new Vector3(a.X, a.Y, -a.Z);
            actual = Vector3.Reflect(a, n);
            Assert.True(MathHelper.Equal(expected, actual), "Vector3.Reflect did not return the expected value.");

            // Reflect on YZ plane.
            n = new Vector3(1.0, 0.0, 0.0);
            expected = new Vector3(-a.X, a.Y, a.Z);
            actual = Vector3.Reflect(a, n);
            Assert.True(MathHelper.Equal(expected, actual), "Vector3.Reflect did not return the expected value.");
        }

        // A test for Reflect (Vector3, Vector3)
        // Reflection when normal and source are the same
        [Fact]
        public void Vector3ReflectTest1()
        {
            Vector3 n = new Vector3(0.45, 1.28, 0.86);
            n = Vector3.Normalize(n);
            Vector3 a = n;

            Vector3 expected = -n;
            Vector3 actual = Vector3.Reflect(a, n);
            Assert.True(MathHelper.Equal(expected, actual), "Vector3.Reflect did not return the expected value.");
        }

        // A test for Reflect (Vector3, Vector3)
        // Reflection when normal and source are negation
        [Fact]
        public void Vector3ReflectTest2()
        {
            Vector3 n = new Vector3(0.45, 1.28, 0.86);
            n = Vector3.Normalize(n);
            Vector3 a = -n;

            Vector3 expected = n;
            Vector3 actual = Vector3.Reflect(a, n);
            Assert.True(MathHelper.Equal(expected, actual), "Vector3.Reflect did not return the expected value.");
        }

        // A test for Reflect (Vector3, Vector3)
        // Reflection when normal and source are perpendicular (a dot n = 0)
        [Fact]
        public void Vector3ReflectTest3()
        {
            Vector3 n = new Vector3(0.45, 1.28, 0.86);
            Vector3 temp = new Vector3(1.28, 0.45, 0.01);
            // find a perpendicular vector of n
            Vector3 a = Vector3.Cross(temp, n);

            Vector3 expected = a;
            Vector3 actual = Vector3.Reflect(a, n);
            Assert.True(MathHelper.Equal(expected, actual), "Vector3.Reflect did not return the expected value.");
        }

        // A test for Transform(Vector3, Matrix4x4)
        [Fact]
        public void Vector3TransformTest()
        {
            Vector3 v = new Vector3(1.0, 2.0, 3.0);
            Matrix4x4 m =
                Matrix4x4.CreateRotationX(MathHelper.ToRadians(30.0)) *
                Matrix4x4.CreateRotationY(MathHelper.ToRadians(30.0)) *
                Matrix4x4.CreateRotationZ(MathHelper.ToRadians(30.0));
            m.M41 = 10.0;
            m.M42 = 20.0;
            m.M43 = 30.0;

            Vector3 expected = new Vector3(12.191987, 21.533493, 32.616024);
            Vector3 actual;

            actual = Vector3.Transform(v, m);
            Assert.True(MathHelper.Equal(expected, actual), "Vector3.Transform did not return the expected value.");
        }

        // A test for Clamp (Vector3, Vector3, Vector3)
        [Fact]
        public void Vector3ClampTest()
        {
            Vector3 a = new Vector3(0.5, 0.3, 0.33);
            Vector3 min = new Vector3(0.0, 0.1, 0.13);
            Vector3 max = new Vector3(1.0, 1.1, 1.13);

            // Normal case.
            // Case N1: specified value is in the range.
            Vector3 expected = new Vector3(0.5, 0.3, 0.33);
            Vector3 actual = Vector3.Clamp(a, min, max);
            Assert.True(MathHelper.Equal(expected, actual), "Vector3.Clamp did not return the expected value.");

            // Normal case.
            // Case N2: specified value is bigger than max value.
            a = new Vector3(2.0, 3.0, 4.0);
            expected = max;
            actual = Vector3.Clamp(a, min, max);
            Assert.True(MathHelper.Equal(expected, actual), "Vector3.Clamp did not return the expected value.");

            // Case N3: specified value is smaller than max value.
            a = new Vector3(-2.0, -3.0, -4.0);
            expected = min;
            actual = Vector3.Clamp(a, min, max);
            Assert.True(MathHelper.Equal(expected, actual), "Vector3.Clamp did not return the expected value.");

            // Case N4: combination case.
            a = new Vector3(-2.0, 0.5, 4.0);
            expected = new Vector3(min.X, a.Y, max.Z);
            actual = Vector3.Clamp(a, min, max);
            Assert.True(MathHelper.Equal(expected, actual), "Vector3.Clamp did not return the expected value.");

            // User specified min value is bigger than max value.
            max = new Vector3(0.0, 0.1, 0.13);
            min = new Vector3(1.0, 1.1, 1.13);

            // Case W1: specified value is in the range.
            a = new Vector3(0.5, 0.3, 0.33);
            expected = min;
            actual = Vector3.Clamp(a, min, max);
            Assert.True(MathHelper.Equal(expected, actual), "Vector3.Clamp did not return the expected value.");

            // Normal case.
            // Case W2: specified value is bigger than max and min value.
            a = new Vector3(2.0, 3.0, 4.0);
            expected = min;
            actual = Vector3.Clamp(a, min, max);
            Assert.True(MathHelper.Equal(expected, actual), "Vector3.Clamp did not return the expected value.");

            // Case W3: specified value is smaller than min and max value.
            a = new Vector3(-2.0, -3.0, -4.0);
            expected = min;
            actual = Vector3.Clamp(a, min, max);
            Assert.True(MathHelper.Equal(expected, actual), "Vector3.Clamp did not return the expected value.");
        }

        // A test for TransformNormal (Vector3, Matrix4x4)
        [Fact]
        public void Vector3TransformNormalTest()
        {
            Vector3 v = new Vector3(1.0, 2.0, 3.0);
            Matrix4x4 m =
                Matrix4x4.CreateRotationX(MathHelper.ToRadians(30.0)) *
                Matrix4x4.CreateRotationY(MathHelper.ToRadians(30.0)) *
                Matrix4x4.CreateRotationZ(MathHelper.ToRadians(30.0));
            m.M41 = 10.0;
            m.M42 = 20.0;
            m.M43 = 30.0;

            Vector3 expected = new Vector3(2.19198728, 1.53349364, 2.61602545);
            Vector3 actual;

            actual = Vector3.TransformNormal(v, m);
            Assert.True(MathHelper.Equal(expected, actual), "Vector3.TransformNormal did not return the expected value.");
        }

        // A test for Transform (Vector3, Quaternion)
        [Fact]
        public void Vector3TransformByQuaternionTest()
        {
            Vector3 v = new Vector3(1.0, 2.0, 3.0);

            Matrix4x4 m =
                Matrix4x4.CreateRotationX(MathHelper.ToRadians(30.0)) *
                Matrix4x4.CreateRotationY(MathHelper.ToRadians(30.0)) *
                Matrix4x4.CreateRotationZ(MathHelper.ToRadians(30.0));
            Quaternion q = Quaternion.CreateFromRotationMatrix(m);

            Vector3 expected = Vector3.Transform(v, m);
            Vector3 actual = Vector3.Transform(v, q);
            Assert.True(MathHelper.Equal(expected, actual), "Vector3.Transform did not return the expected value.");
        }

        // A test for Transform (Vector3, Quaternion)
        // Transform vector3 with zero quaternion
        [Fact]
        public void Vector3TransformByQuaternionTest1()
        {
            Vector3 v = new Vector3(1.0, 2.0, 3.0);
            Quaternion q = new Quaternion();
            Vector3 expected = v;

            Vector3 actual = Vector3.Transform(v, q);
            Assert.True(MathHelper.Equal(expected, actual), "Vector3.Transform did not return the expected value.");
        }

        // A test for Transform (Vector3, Quaternion)
        // Transform vector3 with identity quaternion
        [Fact]
        public void Vector3TransformByQuaternionTest2()
        {
            Vector3 v = new Vector3(1.0, 2.0, 3.0);
            Quaternion q = Quaternion.Identity;
            Vector3 expected = v;

            Vector3 actual = Vector3.Transform(v, q);
            Assert.True(MathHelper.Equal(expected, actual), "Vector3.Transform did not return the expected value.");
        }

        // A test for Normalize (Vector3)
        [Fact]
        public void Vector3NormalizeTest()
        {
            Vector3 a = new Vector3(1.0, 2.0, 3.0);

            Vector3 expected = new Vector3(
                0.26726124191242438468455348087975,
                0.53452248382484876936910696175951,
                0.80178372573727315405366044263926);
            Vector3 actual;

            actual = Vector3.Normalize(a);
            Assert.True(MathHelper.Equal(expected, actual), "Vector3.Normalize did not return the expected value.");
        }

        // A test for Normalize (Vector3)
        // Normalize vector of length one
        [Fact]
        public void Vector3NormalizeTest1()
        {
            Vector3 a = new Vector3(1.0, 0.0, 0.0);

            Vector3 expected = new Vector3(1.0, 0.0, 0.0);
            Vector3 actual = Vector3.Normalize(a);
            Assert.True(MathHelper.Equal(expected, actual), "Vector3.Normalize did not return the expected value.");
        }

        // A test for Normalize (Vector3)
        // Normalize vector of length zero
        [Fact]
        public void Vector3NormalizeTest2()
        {
            Vector3 a = new Vector3(0.0, 0.0, 0.0);

            Vector3 expected = new Vector3(0.0, 0.0, 0.0);
            Vector3 actual = Vector3.Normalize(a);
            Assert.True(double.IsNaN(actual.X) && double.IsNaN(actual.Y) && double.IsNaN(actual.Z), "Vector3.Normalize did not return the expected value.");
        }

        // A test for operator - (Vector3)
        [Fact]
        public void Vector3UnaryNegationTest()
        {
            Vector3 a = new Vector3(1.0, 2.0, 3.0);

            Vector3 expected = new Vector3(-1.0, -2.0, -3.0);
            Vector3 actual;

            actual = -a;

            Assert.True(MathHelper.Equal(expected, actual), "Vector3.operator - did not return the expected value.");
        }

        [Fact]
        public void Vector3UnaryNegationTest1()
        {
            Vector3 a = -new Vector3(Double.NaN, Double.PositiveInfinity, Double.NegativeInfinity);
            Vector3 b = -new Vector3(0.0, 0.0, 0.0);
            Assert.Equal(Double.NaN, a.X);
            Assert.Equal(Double.NegativeInfinity, a.Y);
            Assert.Equal(Double.PositiveInfinity, a.Z);
            Assert.Equal(0.0, b.X);
            Assert.Equal(0.0, b.Y);
            Assert.Equal(0.0, b.Z);
        }

        // A test for operator - (Vector3, Vector3)
        [Fact]
        public void Vector3SubtractionTest()
        {
            Vector3 a = new Vector3(4.0, 2.0, 3.0);

            Vector3 b = new Vector3(1.0, 5.0, 7.0);

            Vector3 expected = new Vector3(3.0, -3.0, -4.0);
            Vector3 actual;

            actual = a - b;

            Assert.True(MathHelper.Equal(expected, actual), "Vector3.operator - did not return the expected value.");
        }

        // A test for operator * (Vector3, double)
        [Fact]
        public void Vector3MultiplyOperatorTest()
        {
            Vector3 a = new Vector3(1.0, 2.0, 3.0);

            double factor = 2.0;

            Vector3 expected = new Vector3(2.0, 4.0, 6.0);
            Vector3 actual;

            actual = a * factor;

            Assert.True(MathHelper.Equal(expected, actual), "Vector3.operator * did not return the expected value.");
        }

        // A test for operator * (double, Vector3)
        [Fact]
        public void Vector3MultiplyOperatorTest2()
        {
            Vector3 a = new Vector3(1.0, 2.0, 3.0);

            const double factor = 2.0;

            Vector3 expected = new Vector3(2.0, 4.0, 6.0);
            Vector3 actual;

            actual = factor * a;

            Assert.True(MathHelper.Equal(expected, actual), "Vector3.operator * did not return the expected value.");
        }

        // A test for operator * (Vector3, Vector3)
        [Fact]
        public void Vector3MultiplyOperatorTest3()
        {
            Vector3 a = new Vector3(1.0, 2.0, 3.0);

            Vector3 b = new Vector3(4.0, 5.0, 6.0);

            Vector3 expected = new Vector3(4.0, 10.0, 18.0);
            Vector3 actual;

            actual = a * b;

            Assert.True(MathHelper.Equal(expected, actual), "Vector3.operator * did not return the expected value.");
        }

        // A test for operator / (Vector3, double)
        [Fact]
        public void Vector3DivisionTest()
        {
            Vector3 a = new Vector3(1.0, 2.0, 3.0);

            double div = 2.0;

            Vector3 expected = new Vector3(0.5, 1.0, 1.5);
            Vector3 actual;

            actual = a / div;

            Assert.True(MathHelper.Equal(expected, actual), "Vector3.operator / did not return the expected value.");
        }

        // A test for operator / (Vector3, Vector3)
        [Fact]
        public void Vector3DivisionTest1()
        {
            Vector3 a = new Vector3(4.0, 2.0, 3.0);

            Vector3 b = new Vector3(1.0, 5.0, 6.0);

            Vector3 expected = new Vector3(4.0, 0.4, 0.5);
            Vector3 actual;

            actual = a / b;

            Assert.True(MathHelper.Equal(expected, actual), "Vector3.operator / did not return the expected value.");
        }

        // A test for operator / (Vector3, Vector3)
        // Divide by zero
        [Fact]
        public void Vector3DivisionTest2()
        {
            Vector3 a = new Vector3(-2.0, 3.0, double.MaxValue);

            double div = 0.0;

            Vector3 actual = a / div;

            Assert.True(double.IsNegativeInfinity(actual.X), "Vector3.operator / did not return the expected value.");
            Assert.True(double.IsPositiveInfinity(actual.Y), "Vector3.operator / did not return the expected value.");
            Assert.True(double.IsPositiveInfinity(actual.Z), "Vector3.operator / did not return the expected value.");
        }

        // A test for operator / (Vector3, Vector3)
        // Divide by zero
        [Fact]
        public void Vector3DivisionTest3()
        {
            Vector3 a = new Vector3(0.047, -3.0, double.NegativeInfinity);
            Vector3 b = new Vector3();

            Vector3 actual = a / b;

            Assert.True(double.IsPositiveInfinity(actual.X), "Vector3.operator / did not return the expected value.");
            Assert.True(double.IsNegativeInfinity(actual.Y), "Vector3.operator / did not return the expected value.");
            Assert.True(double.IsNegativeInfinity(actual.Z), "Vector3.operator / did not return the expected value.");
        }

        // A test for operator + (Vector3, Vector3)
        [Fact]
        public void Vector3AdditionTest()
        {
            Vector3 a = new Vector3(1.0, 2.0, 3.0);
            Vector3 b = new Vector3(4.0, 5.0, 6.0);

            Vector3 expected = new Vector3(5.0, 7.0, 9.0);
            Vector3 actual;

            actual = a + b;

            Assert.True(MathHelper.Equal(expected, actual), "Vector3.operator + did not return the expected value.");
        }

        // A test for Vector3 (double, double, double)
        [Fact]
        public void Vector3ConstructorTest()
        {
            double x = 1.0;
            double y = 2.0;
            double z = 3.0;

            Vector3 target = new Vector3(x, y, z);
            Assert.True(MathHelper.Equal(target.X, x) && MathHelper.Equal(target.Y, y) && MathHelper.Equal(target.Z, z), "Vector3.constructor (x,y,z) did not return the expected value.");
        }

        // A test for Vector3 (Vector2, double)
        [Fact]
        public void Vector3ConstructorTest1()
        {
            Vector2 a = new Vector2(1.0, 2.0);

            double z = 3.0;

            Vector3 target = new Vector3(a, z);
            Assert.True(MathHelper.Equal(target.X, a.X) && MathHelper.Equal(target.Y, a.Y) && MathHelper.Equal(target.Z, z), "Vector3.constructor (Vector2,z) did not return the expected value.");
        }

        // A test for Vector3 ()
        // Constructor with no parameter
        [Fact]
        public void Vector3ConstructorTest3()
        {
            Vector3 a = new Vector3();

            Assert.Equal(a.X, 0.0);
            Assert.Equal(a.Y, 0.0);
            Assert.Equal(a.Z, 0.0);
        }

        // A test for Vector2 (double, double)
        // Constructor with special floating values
        [Fact]
        public void Vector3ConstructorTest4()
        {
            Vector3 target = new Vector3(double.NaN, double.MaxValue, double.PositiveInfinity);

            Assert.True(double.IsNaN(target.X), "Vector3.constructor (Vector3) did not return the expected value.");
            Assert.True(double.Equals(double.MaxValue, target.Y), "Vector3.constructor (Vector3) did not return the expected value.");
            Assert.True(double.IsPositiveInfinity(target.Z), "Vector3.constructor (Vector3) did not return the expected value.");
        }

        // A test for Add (Vector3, Vector3)
        [Fact]
        public void Vector3AddTest()
        {
            Vector3 a = new Vector3(1.0, 2.0, 3.0);
            Vector3 b = new Vector3(5.0, 6.0, 7.0);

            Vector3 expected = new Vector3(6.0, 8.0, 10.0);
            Vector3 actual;

            actual = Vector3.Add(a, b);
            Assert.Equal(expected, actual);
        }

        // A test for Divide (Vector3, double)
        [Fact]
        public void Vector3DivideTest()
        {
            Vector3 a = new Vector3(1.0, 2.0, 3.0);
            double div = 2.0;
            Vector3 expected = new Vector3(0.5, 1.0, 1.5);
            Vector3 actual;
            actual = Vector3.Divide(a, div);
            Assert.Equal(expected, actual);
        }

        // A test for Divide (Vector3, Vector3)
        [Fact]
        public void Vector3DivideTest1()
        {
            Vector3 a = new Vector3(1.0, 6.0, 7.0);
            Vector3 b = new Vector3(5.0, 2.0, 3.0);

            Vector3 expected = new Vector3(1.0 / 5.0, 6.0 / 2.0, 7.0 / 3.0);
            Vector3 actual;

            actual = Vector3.Divide(a, b);
            Assert.Equal(expected, actual);
        }

        // A test for Equals (object)
        [Fact]
        public void Vector3EqualsTest()
        {
            Vector3 a = new Vector3(1.0, 2.0, 3.0);
            Vector3 b = new Vector3(1.0, 2.0, 3.0);

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

        // A test for Multiply (Vector3, double)
        [Fact]
        public void Vector3MultiplyTest()
        {
            Vector3 a = new Vector3(1.0, 2.0, 3.0);
            const double factor = 2.0;
            Vector3 expected = new Vector3(2.0, 4.0, 6.0);
            Vector3 actual = Vector3.Multiply(a, factor);
            Assert.Equal(expected, actual);
        }

        // A test for Multiply (double, Vector3)
        [Fact]
        public static void Vector3MultiplyTest2()
        {
            Vector3 a = new Vector3(1.0, 2.0, 3.0);
            const double factor = 2.0;
            Vector3 expected = new Vector3(2.0, 4.0, 6.0);
            Vector3 actual = Vector3.Multiply(factor, a);
            Assert.Equal(expected, actual);
        }

        // A test for Multiply (Vector3, Vector3)
        [Fact]
        public void Vector3MultiplyTest3()
        {
            Vector3 a = new Vector3(1.0, 2.0, 3.0);
            Vector3 b = new Vector3(5.0, 6.0, 7.0);

            Vector3 expected = new Vector3(5.0, 12.0, 21.0);
            Vector3 actual;

            actual = Vector3.Multiply(a, b);
            Assert.Equal(expected, actual);
        }

        // A test for Negate (Vector3)
        [Fact]
        public void Vector3NegateTest()
        {
            Vector3 a = new Vector3(1.0, 2.0, 3.0);

            Vector3 expected = new Vector3(-1.0, -2.0, -3.0);
            Vector3 actual;

            actual = Vector3.Negate(a);
            Assert.Equal(expected, actual);
        }

        // A test for operator != (Vector3, Vector3)
        [Fact]
        public void Vector3InequalityTest()
        {
            Vector3 a = new Vector3(1.0, 2.0, 3.0);
            Vector3 b = new Vector3(1.0, 2.0, 3.0);

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

        // A test for operator == (Vector3, Vector3)
        [Fact]
        public void Vector3EqualityTest()
        {
            Vector3 a = new Vector3(1.0, 2.0, 3.0);
            Vector3 b = new Vector3(1.0, 2.0, 3.0);

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

        // A test for Subtract (Vector3, Vector3)
        [Fact]
        public void Vector3SubtractTest()
        {
            Vector3 a = new Vector3(1.0, 6.0, 3.0);
            Vector3 b = new Vector3(5.0, 2.0, 3.0);

            Vector3 expected = new Vector3(-4.0, 4.0, 0.0);
            Vector3 actual;

            actual = Vector3.Subtract(a, b);
            Assert.Equal(expected, actual);
        }

        // A test for One
        [Fact]
        public void Vector3OneTest()
        {
            Vector3 val = new Vector3(1.0, 1.0, 1.0);
            Assert.Equal(val, Vector3.One);
        }

        // A test for UnitX
        [Fact]
        public void Vector3UnitXTest()
        {
            Vector3 val = new Vector3(1.0, 0.0, 0.0);
            Assert.Equal(val, Vector3.UnitX);
        }

        // A test for UnitY
        [Fact]
        public void Vector3UnitYTest()
        {
            Vector3 val = new Vector3(0.0, 1.0, 0.0);
            Assert.Equal(val, Vector3.UnitY);
        }

        // A test for UnitZ
        [Fact]
        public void Vector3UnitZTest()
        {
            Vector3 val = new Vector3(0.0, 0.0, 1.0);
            Assert.Equal(val, Vector3.UnitZ);
        }

        // A test for Zero
        [Fact]
        public void Vector3ZeroTest()
        {
            Vector3 val = new Vector3(0.0, 0.0, 0.0);
            Assert.Equal(val, Vector3.Zero);
        }

        // A test for Equals (Vector3)
        [Fact]
        public void Vector3EqualsTest1()
        {
            Vector3 a = new Vector3(1.0, 2.0, 3.0);
            Vector3 b = new Vector3(1.0, 2.0, 3.0);

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

        // A test for Vector3 (double)
        [Fact]
        public void Vector3ConstructorTest5()
        {
            double value = 1.0;
            Vector3 target = new Vector3(value);

            Vector3 expected = new Vector3(value, value, value);
            Assert.Equal(expected, target);

            value = 2.0;
            target = new Vector3(value);
            expected = new Vector3(value, value, value);
            Assert.Equal(expected, target);
        }

        // A test for Vector3 comparison involving NaN values
        [Fact]
        public void Vector3EqualsNanTest()
        {
            Vector3 a = new Vector3(double.NaN, 0, 0);
            Vector3 b = new Vector3(0, double.NaN, 0);
            Vector3 c = new Vector3(0, 0, double.NaN);

            Assert.False(a == Vector3.Zero);
            Assert.False(b == Vector3.Zero);
            Assert.False(c == Vector3.Zero);

            Assert.True(a != Vector3.Zero);
            Assert.True(b != Vector3.Zero);
            Assert.True(c != Vector3.Zero);

            Assert.False(a.Equals(Vector3.Zero));
            Assert.False(b.Equals(Vector3.Zero));
            Assert.False(c.Equals(Vector3.Zero));

            // Counterintuitive result - IEEE rules for NaN comparison are weird!
            Assert.False(a.Equals(a));
            Assert.False(b.Equals(b));
            Assert.False(c.Equals(c));
        }

        [Fact]
        public void Vector3AbsTest()
        {
            Vector3 v1 = new Vector3(-2.5, 2.0, 0.5);
            Vector3 v3 = Vector3.Abs(new Vector3(0.0, Double.NegativeInfinity, Double.NaN));
            Vector3 v = Vector3.Abs(v1);
            Assert.Equal(2.5, v.X);
            Assert.Equal(2.0, v.Y);
            Assert.Equal(0.5, v.Z);
            Assert.Equal(0.0, v3.X);
            Assert.Equal(Double.PositiveInfinity, v3.Y);
            Assert.Equal(Double.NaN, v3.Z);
        }

        [Fact]
        public void Vector3SqrtTest()
        {
            Vector3 a = new Vector3(-2.5, 2.0, 0.5);
            Vector3 b = new Vector3(5.5, 4.5, 16.5);
            Assert.Equal(2, (int)Vector3.SquareRoot(b).X);
            Assert.Equal(2, (int)Vector3.SquareRoot(b).Y);
            Assert.Equal(4, (int)Vector3.SquareRoot(b).Z);
            Assert.Equal(Double.NaN, Vector3.SquareRoot(a).X);
        }

        // A test to make sure these types are blittable directly into GPU buffer memory layouts
        [Fact]
        public unsafe void Vector3SizeofTest()
        {
            Assert.Equal(12*2, sizeof(Vector3));
            Assert.Equal(24*2, sizeof(Vector3_2x));
            Assert.Equal(16*2, sizeof(Vector3PlusFloat));
            Assert.Equal(32*2, sizeof(Vector3PlusFloat_2x));
        }

        [StructLayout(LayoutKind.Sequential)]
        struct Vector3_2x
        {
            private Vector3 _a;
            private Vector3 _b;
        }

        [StructLayout(LayoutKind.Sequential)]
        struct Vector3PlusFloat
        {
            private Vector3 _v;
            private double _f;
        }

        [StructLayout(LayoutKind.Sequential)]
        struct Vector3PlusFloat_2x
        {
            private Vector3PlusFloat _a;
            private Vector3PlusFloat _b;
        }

        [Fact]
        public void SetFieldsTest()
        {
            Vector3 v3 = new Vector3(4, 5, 6);
            v3.X = 1.0;
            v3.Y = 2.0;
            v3.Z = 3.0;
            Assert.Equal(1.0, v3.X);
            Assert.Equal(2.0, v3.Y);
            Assert.Equal(3.0, v3.Z);
            Vector3 v4 = v3;
            v4.Y = 0.5;
            v4.Z = 2.2;
            Assert.Equal(1.0, v4.X);
            Assert.Equal(0.5, v4.Y);
            Assert.Equal(2.2, v4.Z);
            Assert.Equal(2.0, v3.Y);

            Vector3 before = new Vector3(1, 2, 3);
            Vector3 after = before;
            after.X = 500.0;
            Assert.NotEqual(before, after);
        }

        [Fact]
        public void EmbeddedVectorSetFields()
        {
            EmbeddedVectorObject evo = new EmbeddedVectorObject();
            evo.FieldVector.X = 5.0;
            evo.FieldVector.Y = 5.0;
            evo.FieldVector.Z = 5.0;
            Assert.Equal(5.0, evo.FieldVector.X);
            Assert.Equal(5.0, evo.FieldVector.Y);
            Assert.Equal(5.0, evo.FieldVector.Z);
        }

        private class EmbeddedVectorObject
        {
            public Vector3 FieldVector;
        }
    }
}
