// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using System.DoubleNumerics;
using System.Globalization;
using System.Runtime.InteropServices;
using Xunit;

namespace System.Numerics.Tests
{
    public class Vector4Tests
    {
        [Fact]
        public void Vector4MarshalSizeTest()
        {
            Assert.Equal(16*2, Marshal.SizeOf<Vector4>());
            Assert.Equal(16*2, Marshal.SizeOf<Vector4>(new Vector4()));
        }

        [Fact]
        public void Vector4CopyToTest()
        {
            Vector4 v1 = new Vector4(2.5, 2.0, 3.0, 3.3);

            double[] a = new double[5];
            double[] b = new double[4];

            Assert.Throws<NullReferenceException>(() => v1.CopyTo(null, 0));
            Assert.Throws<ArgumentOutOfRangeException>(() => v1.CopyTo(a, -1));
            Assert.Throws<ArgumentOutOfRangeException>(() => v1.CopyTo(a, a.Length));
            Assert.Throws<ArgumentException>(() => v1.CopyTo(a, a.Length - 2));

            v1.CopyTo(a, 1);
            v1.CopyTo(b);
            Assert.Equal(0.0, a[0]);
            Assert.Equal(2.5, a[1]);
            Assert.Equal(2.0, a[2]);
            Assert.Equal(3.0, a[3]);
            Assert.Equal(3.3, a[4]);
            Assert.Equal(2.5, b[0]);
            Assert.Equal(2.0, b[1]);
            Assert.Equal(3.0, b[2]);
            Assert.Equal(3.3, b[3]);
        }

        [Fact]
        public void Vector4GetHashCodeTest()
        {
            Vector4 v1 = new Vector4(2.5, 2.0, 3.0, 3.3);
            Vector4 v2 = new Vector4(2.5, 2.0, 3.0, 3.3);
            Vector4 v3 = new Vector4(2.5, 2.0, 3.0, 3.3);
            Vector4 v5 = new Vector4(3.3, 3.0, 2.0, 2.5);
            Assert.Equal(v1.GetHashCode(), v1.GetHashCode());
            Assert.Equal(v1.GetHashCode(), v2.GetHashCode());
            Assert.NotEqual(v1.GetHashCode(), v5.GetHashCode());
            Assert.Equal(v1.GetHashCode(), v3.GetHashCode());
            Vector4 v4 = new Vector4(0.0, 0.0, 0.0, 0.0);
            Vector4 v6 = new Vector4(1.0, 0.0, 0.0, 0.0);
            Vector4 v7 = new Vector4(0.0, 1.0, 0.0, 0.0);
            Vector4 v8 = new Vector4(1.0, 1.0, 1.0, 1.0);
            Vector4 v9 = new Vector4(1.0, 1.0, 0.0, 0.0);
            Assert.NotEqual(v4.GetHashCode(), v6.GetHashCode());
            Assert.NotEqual(v4.GetHashCode(), v7.GetHashCode());
            Assert.NotEqual(v4.GetHashCode(), v8.GetHashCode());
            Assert.NotEqual(v7.GetHashCode(), v6.GetHashCode());
            Assert.NotEqual(v8.GetHashCode(), v6.GetHashCode());
            Assert.NotEqual(v8.GetHashCode(), v7.GetHashCode());
            Assert.NotEqual(v9.GetHashCode(), v7.GetHashCode());
        }

        [Fact]
        public void Vector4ToStringTest()
        {
            string separator = CultureInfo.CurrentCulture.NumberFormat.NumberGroupSeparator;
            CultureInfo enUsCultureInfo = new CultureInfo("en-US");

            Vector4 v1 = new Vector4(2.5, 2.0, 3.0, 3.3);

            string v1str = v1.ToString();
            string expectedv1 = string.Format(CultureInfo.CurrentCulture
                , "<{1:G}{0} {2:G}{0} {3:G}{0} {4:G}>"
                , separator, 2.5, 2, 3, 3.3);
            Assert.Equal(expectedv1, v1str);

            string v1strformatted = v1.ToString("c", CultureInfo.CurrentCulture);
            string expectedv1formatted = string.Format(CultureInfo.CurrentCulture
                , "<{1:c}{0} {2:c}{0} {3:c}{0} {4:c}>"
                , separator, 2.5, 2, 3, 3.3);
            Assert.Equal(expectedv1formatted, v1strformatted);

            string v2strformatted = v1.ToString("c", enUsCultureInfo);
            string expectedv2formatted = string.Format(enUsCultureInfo
                , "<{1:c}{0} {2:c}{0} {3:c}{0} {4:c}>"
                , enUsCultureInfo.NumberFormat.NumberGroupSeparator, 2.5, 2, 3, 3.3);
            Assert.Equal(expectedv2formatted, v2strformatted);

            string v3strformatted = v1.ToString("c");
            string expectedv3formatted = string.Format(CultureInfo.CurrentCulture
                , "<{1:c}{0} {2:c}{0} {3:c}{0} {4:c}>"
                , separator, 2.5, 2, 3, 3.3);
            Assert.Equal(expectedv3formatted, v3strformatted);
        }

        // A test for DistanceSquared (Vector4, Vector4)
        [Fact]
        public void Vector4DistanceSquaredTest()
        {
            Vector4 a = new Vector4(1.0, 2.0, 3.0, 4.0);
            Vector4 b = new Vector4(5.0, 6.0, 7.0, 8.0);

            double expected = 64.0;
            double actual;

            actual = Vector4.DistanceSquared(a, b);
            Assert.True(MathHelper.Equal(expected, actual), "Vector4.DistanceSquared did not return the expected value.");
        }

        // A test for Distance (Vector4, Vector4)
        [Fact]
        public void Vector4DistanceTest()
        {
            Vector4 a = new Vector4(1.0, 2.0, 3.0, 4.0);
            Vector4 b = new Vector4(5.0, 6.0, 7.0, 8.0);

            double expected = 8.0;
            double actual;

            actual = Vector4.Distance(a, b);
            Assert.True(MathHelper.Equal(expected, actual), "Vector4.Distance did not return the expected value.");
        }

        // A test for Distance (Vector4, Vector4)
        // Distance from the same point
        [Fact]
        public void Vector4DistanceTest1()
        {
            Vector4 a = new Vector4(new Vector2(1.051, 2.05), 3.478, 1.0);
            Vector4 b = new Vector4(new Vector3(1.051, 2.05, 3.478), 0.0);
            b.W = 1.0;

            double actual = Vector4.Distance(a, b);
            Assert.Equal(0.0, actual);
        }

        // A test for Dot (Vector4, Vector4)
        [Fact]
        public void Vector4DotTest()
        {
            Vector4 a = new Vector4(1.0, 2.0, 3.0, 4.0);
            Vector4 b = new Vector4(5.0, 6.0, 7.0, 8.0);

            double expected = 70.0;
            double actual;

            actual = Vector4.Dot(a, b);
            Assert.True(MathHelper.Equal(expected, actual), "Vector4.Dot did not return the expected value.");
        }

        // A test for Dot (Vector4, Vector4)
        // Dot test for perpendicular vector
        [Fact]
        public void Vector4DotTest1()
        {
            Vector3 a = new Vector3(1.55, 1.55, 1);
            Vector3 b = new Vector3(2.5, 3, 1.5);
            Vector3 c = Vector3.Cross(a, b);

            Vector4 d = new Vector4(a, 0);
            Vector4 e = new Vector4(c, 0);

            double actual = Vector4.Dot(d, e);
            Assert.True(MathHelper.Equal(0.0, actual), "Vector4.Dot did not return the expected value.");
        }

        // A test for Length ()
        [Fact]
        public void Vector4LengthTest()
        {
            Vector3 a = new Vector3(1.0, 2.0, 3.0);
            double w = 4.0;

            Vector4 target = new Vector4(a, w);

            double expected = (double)Math.Sqrt(30.0);
            double actual;

            actual = target.Length();

            Assert.True(MathHelper.Equal(expected, actual), "Vector4.Length did not return the expected value.");
        }

        // A test for Length ()
        // Length test where length is zero
        [Fact]
        public void Vector4LengthTest1()
        {
            Vector4 target = new Vector4();

            double expected = 0.0;
            double actual = target.Length();

            Assert.True(MathHelper.Equal(expected, actual), "Vector4.Length did not return the expected value.");
        }

        // A test for LengthSquared ()
        [Fact]
        public void Vector4LengthSquaredTest()
        {
            Vector3 a = new Vector3(1.0, 2.0, 3.0);
            double w = 4.0;

            Vector4 target = new Vector4(a, w);

            double expected = 30;
            double actual;

            actual = target.LengthSquared();

            Assert.True(MathHelper.Equal(expected, actual), "Vector4.LengthSquared did not return the expected value.");
        }

        // A test for Min (Vector4, Vector4)
        [Fact]
        public void Vector4MinTest()
        {
            Vector4 a = new Vector4(-1.0, 4.0, -3.0, 1000.0);
            Vector4 b = new Vector4(2.0, 1.0, -1.0, 0.0);

            Vector4 expected = new Vector4(-1.0, 1.0, -3.0, 0.0);
            Vector4 actual;
            actual = Vector4.Min(a, b);
            Assert.True(MathHelper.Equal(expected, actual), "Vector4.Min did not return the expected value.");
        }

        // A test for Max (Vector4, Vector4)
        [Fact]
        public void Vector4MaxTest()
        {
            Vector4 a = new Vector4(-1.0, 4.0, -3.0, 1000.0);
            Vector4 b = new Vector4(2.0, 1.0, -1.0, 0.0);

            Vector4 expected = new Vector4(2.0, 4.0, -1.0, 1000.0);
            Vector4 actual;
            actual = Vector4.Max(a, b);
            Assert.True(MathHelper.Equal(expected, actual), "Vector4.Max did not return the expected value.");
        }

        [Fact]
        public void Vector4MinMaxCodeCoverageTest()
        {
            Vector4 min = Vector4.Zero;
            Vector4 max = Vector4.One;
            Vector4 actual;

            // Min.
            actual = Vector4.Min(min, max);
            Assert.Equal(actual, min);

            actual = Vector4.Min(max, min);
            Assert.Equal(actual, min);

            // Max.
            actual = Vector4.Max(min, max);
            Assert.Equal(actual, max);

            actual = Vector4.Max(max, min);
            Assert.Equal(actual, max);
        }

        // A test for Clamp (Vector4, Vector4, Vector4)
        [Fact]
        public void Vector4ClampTest()
        {
            Vector4 a = new Vector4(0.5, 0.3, 0.33, 0.44);
            Vector4 min = new Vector4(0.0, 0.1, 0.13, 0.14);
            Vector4 max = new Vector4(1.0, 1.1, 1.13, 1.14);

            // Normal case.
            // Case N1: specified value is in the range.
            Vector4 expected = new Vector4(0.5, 0.3, 0.33, 0.44);
            Vector4 actual = Vector4.Clamp(a, min, max);
            Assert.True(MathHelper.Equal(expected, actual), "Vector4.Clamp did not return the expected value.");

            // Normal case.
            // Case N2: specified value is bigger than max value.
            a = new Vector4(2.0, 3.0, 4.0, 5.0);
            expected = max;
            actual = Vector4.Clamp(a, min, max);
            Assert.True(MathHelper.Equal(expected, actual), "Vector4.Clamp did not return the expected value.");

            // Case N3: specified value is smaller than max value.
            a = new Vector4(-2.0, -3.0, -4.0, -5.0);
            expected = min;
            actual = Vector4.Clamp(a, min, max);
            Assert.True(MathHelper.Equal(expected, actual), "Vector4.Clamp did not return the expected value.");

            // Case N4: combination case.
            a = new Vector4(-2.0, 0.5, 4.0, -5.0);
            expected = new Vector4(min.X, a.Y, max.Z, min.W);
            actual = Vector4.Clamp(a, min, max);
            Assert.True(MathHelper.Equal(expected, actual), "Vector4.Clamp did not return the expected value.");

            // User specified min value is bigger than max value.
            max = new Vector4(0.0, 0.1, 0.13, 0.14);
            min = new Vector4(1.0, 1.1, 1.13, 1.14);

            // Case W1: specified value is in the range.
            a = new Vector4(0.5, 0.3, 0.33, 0.44);
            expected = min;
            actual = Vector4.Clamp(a, min, max);
            Assert.True(MathHelper.Equal(expected, actual), "Vector4.Clamp did not return the expected value.");

            // Normal case.
            // Case W2: specified value is bigger than max and min value.
            a = new Vector4(2.0, 3.0, 4.0, 5.0);
            expected = min;
            actual = Vector4.Clamp(a, min, max);
            Assert.True(MathHelper.Equal(expected, actual), "Vector4.Clamp did not return the expected value.");

            // Case W3: specified value is smaller than min and max value.
            a = new Vector4(-2.0, -3.0, -4.0, -5.0);
            expected = min;
            actual = Vector4.Clamp(a, min, max);
            Assert.True(MathHelper.Equal(expected, actual), "Vector4.Clamp did not return the expected value.");
        }

        // A test for Lerp (Vector4, Vector4, double)
        [Fact]
        public void Vector4LerpTest()
        {
            Vector4 a = new Vector4(1.0, 2.0, 3.0, 4.0);
            Vector4 b = new Vector4(5.0, 6.0, 7.0, 8.0);

            double t = 0.5;

            Vector4 expected = new Vector4(3.0, 4.0, 5.0, 6.0);
            Vector4 actual;

            actual = Vector4.Lerp(a, b, t);
            Assert.True(MathHelper.Equal(expected, actual), "Vector4.Lerp did not return the expected value.");
        }

        // A test for Lerp (Vector4, Vector4, double)
        // Lerp test with factor zero
        [Fact]
        public void Vector4LerpTest1()
        {
            Vector4 a = new Vector4(new Vector3(1.0, 2.0, 3.0), 4.0);
            Vector4 b = new Vector4(4.0, 5.0, 6.0, 7.0);

            double t = 0.0;
            Vector4 expected = new Vector4(1.0, 2.0, 3.0, 4.0);
            Vector4 actual = Vector4.Lerp(a, b, t);
            Assert.True(MathHelper.Equal(expected, actual), "Vector4.Lerp did not return the expected value.");
        }

        // A test for Lerp (Vector4, Vector4, double)
        // Lerp test with factor one
        [Fact]
        public void Vector4LerpTest2()
        {
            Vector4 a = new Vector4(new Vector3(1.0, 2.0, 3.0), 4.0);
            Vector4 b = new Vector4(4.0, 5.0, 6.0, 7.0);

            double t = 1.0;
            Vector4 expected = new Vector4(4.0, 5.0, 6.0, 7.0);
            Vector4 actual = Vector4.Lerp(a, b, t);
            Assert.True(MathHelper.Equal(expected, actual), "Vector4.Lerp did not return the expected value.");
        }

        // A test for Lerp (Vector4, Vector4, double)
        // Lerp test with factor > 1
        [Fact]
        public void Vector4LerpTest3()
        {
            Vector4 a = new Vector4(new Vector3(0.0, 0.0, 0.0), 0.0);
            Vector4 b = new Vector4(4.0, 5.0, 6.0, 7.0);

            double t = 2.0;
            Vector4 expected = new Vector4(8.0, 10.0, 12.0, 14.0);
            Vector4 actual = Vector4.Lerp(a, b, t);
            Assert.True(MathHelper.Equal(expected, actual), "Vector4.Lerp did not return the expected value.");
        }

        // A test for Lerp (Vector4, Vector4, double)
        // Lerp test with factor < 0
        [Fact]
        public void Vector4LerpTest4()
        {
            Vector4 a = new Vector4(new Vector3(0.0, 0.0, 0.0), 0.0);
            Vector4 b = new Vector4(4.0, 5.0, 6.0, 7.0);

            double t = -2.0;
            Vector4 expected = -(b * 2);
            Vector4 actual = Vector4.Lerp(a, b, t);
            Assert.True(MathHelper.Equal(expected, actual), "Vector4.Lerp did not return the expected value.");
        }

        // A test for Lerp (Vector4, Vector4, double)
        // Lerp test from the same point
        [Fact]
        public void Vector4LerpTest5()
        {
            Vector4 a = new Vector4(4.0, 5.0, 6.0, 7.0);
            Vector4 b = new Vector4(4.0, 5.0, 6.0, 7.0);

            double t = 0.85;
            Vector4 expected = a;
            Vector4 actual = Vector4.Lerp(a, b, t);
            Assert.True(MathHelper.Equal(expected, actual), "Vector4.Lerp did not return the expected value.");
        }

        // A test for Transform (Vector2, Matrix4x4)
        [Fact]
        public void Vector4TransformTest1()
        {
            Vector2 v = new Vector2(1.0, 2.0);

            Matrix4x4 m =
                Matrix4x4.CreateRotationX(MathHelper.ToRadians(30.0)) *
                Matrix4x4.CreateRotationY(MathHelper.ToRadians(30.0)) *
                Matrix4x4.CreateRotationZ(MathHelper.ToRadians(30.0));
            m.M41 = 10.0;
            m.M42 = 20.0;
            m.M43 = 30.0;

            Vector4 expected = new Vector4(10.316987, 22.183012, 30.3660259, 1.0);
            Vector4 actual;

            actual = Vector4.Transform(v, m);
            Assert.True(MathHelper.Equal(expected, actual), "Vector4.Transform did not return the expected value.");
        }

        // A test for Transform (Vector3, Matrix4x4)
        [Fact]
        public void Vector4TransformTest2()
        {
            Vector3 v = new Vector3(1.0, 2.0, 3.0);

            Matrix4x4 m =
                Matrix4x4.CreateRotationX(MathHelper.ToRadians(30.0)) *
                Matrix4x4.CreateRotationY(MathHelper.ToRadians(30.0)) *
                Matrix4x4.CreateRotationZ(MathHelper.ToRadians(30.0));
            m.M41 = 10.0;
            m.M42 = 20.0;
            m.M43 = 30.0;

            Vector4 expected = new Vector4(12.19198728, 21.53349376, 32.61602545, 1.0);
            Vector4 actual;

            actual = Vector4.Transform(v, m);
            Assert.True(MathHelper.Equal(expected, actual), "vector4.Transform did not return the expected value.");
        }

        // A test for Transform (Vector4, Matrix4x4)
        [Fact]
        public void Vector4TransformVector4Test()
        {
            Vector4 v = new Vector4(1.0, 2.0, 3.0, 0.0);

            Matrix4x4 m =
                Matrix4x4.CreateRotationX(MathHelper.ToRadians(30.0)) *
                Matrix4x4.CreateRotationY(MathHelper.ToRadians(30.0)) *
                Matrix4x4.CreateRotationZ(MathHelper.ToRadians(30.0));
            m.M41 = 10.0;
            m.M42 = 20.0;
            m.M43 = 30.0;

            Vector4 expected = new Vector4(2.19198728, 1.53349376, 2.61602545, 0.0);
            Vector4 actual;

            actual = Vector4.Transform(v, m);
            Assert.True(MathHelper.Equal(expected, actual), "Vector4.Transform did not return the expected value.");

            // 
            v.W = 1.0;

            expected = new Vector4(12.19198728, 21.53349376, 32.61602545, 1.0);
            actual = Vector4.Transform(v, m);
            Assert.True(MathHelper.Equal(expected, actual), "Vector4.Transform did not return the expected value.");
        }

        // A test for Transform (Vector4, Matrix4x4)
        // Transform vector4 with zero matrix
        [Fact]
        public void Vector4TransformVector4Test1()
        {
            Vector4 v = new Vector4(1.0, 2.0, 3.0, 0.0);
            Matrix4x4 m = new Matrix4x4();
            Vector4 expected = new Vector4(0, 0, 0, 0);

            Vector4 actual = Vector4.Transform(v, m);
            Assert.True(MathHelper.Equal(expected, actual), "Vector4.Transform did not return the expected value.");
        }

        // A test for Transform (Vector4, Matrix4x4)
        // Transform vector4 with identity matrix
        [Fact]
        public void Vector4TransformVector4Test2()
        {
            Vector4 v = new Vector4(1.0, 2.0, 3.0, 0.0);
            Matrix4x4 m = Matrix4x4.Identity;
            Vector4 expected = new Vector4(1.0, 2.0, 3.0, 0.0);

            Vector4 actual = Vector4.Transform(v, m);
            Assert.True(MathHelper.Equal(expected, actual), "Vector4.Transform did not return the expected value.");
        }

        // A test for Transform (Vector3, Matrix4x4)
        // Transform Vector3 test
        [Fact]
        public void Vector4TransformVector3Test()
        {
            Vector3 v = new Vector3(1.0, 2.0, 3.0);

            Matrix4x4 m =
                Matrix4x4.CreateRotationX(MathHelper.ToRadians(30.0)) *
                Matrix4x4.CreateRotationY(MathHelper.ToRadians(30.0)) *
                Matrix4x4.CreateRotationZ(MathHelper.ToRadians(30.0));
            m.M41 = 10.0;
            m.M42 = 20.0;
            m.M43 = 30.0;

            Vector4 expected = Vector4.Transform(new Vector4(v, 1.0), m);
            Vector4 actual = Vector4.Transform(v, m);
            Assert.True(MathHelper.Equal(expected, actual), "Vector4.Transform did not return the expected value.");
        }

        // A test for Transform (Vector3, Matrix4x4)
        // Transform vector3 with zero matrix
        [Fact]
        public void Vector4TransformVector3Test1()
        {
            Vector3 v = new Vector3(1.0, 2.0, 3.0);
            Matrix4x4 m = new Matrix4x4();
            Vector4 expected = new Vector4(0, 0, 0, 0);

            Vector4 actual = Vector4.Transform(v, m);
            Assert.True(MathHelper.Equal(expected, actual), "Vector4.Transform did not return the expected value.");
        }

        // A test for Transform (Vector3, Matrix4x4)
        // Transform vector3 with identity matrix
        [Fact]
        public void Vector4TransformVector3Test2()
        {
            Vector3 v = new Vector3(1.0, 2.0, 3.0);
            Matrix4x4 m = Matrix4x4.Identity;
            Vector4 expected = new Vector4(1.0, 2.0, 3.0, 1.0);

            Vector4 actual = Vector4.Transform(v, m);
            Assert.True(MathHelper.Equal(expected, actual), "Vector4.Transform did not return the expected value.");
        }

        // A test for Transform (Vector2, Matrix4x4)
        // Transform Vector2 test
        [Fact]
        public void Vector4TransformVector2Test()
        {
            Vector2 v = new Vector2(1.0, 2.0);

            Matrix4x4 m =
                Matrix4x4.CreateRotationX(MathHelper.ToRadians(30.0)) *
                Matrix4x4.CreateRotationY(MathHelper.ToRadians(30.0)) *
                Matrix4x4.CreateRotationZ(MathHelper.ToRadians(30.0));
            m.M41 = 10.0;
            m.M42 = 20.0;
            m.M43 = 30.0;

            Vector4 expected = Vector4.Transform(new Vector4(v, 0.0, 1.0), m);
            Vector4 actual = Vector4.Transform(v, m);
            Assert.True(MathHelper.Equal(expected, actual), "Vector4.Transform did not return the expected value.");
        }

        // A test for Transform (Vector2, Matrix4x4)
        // Transform Vector2 with zero matrix
        [Fact]
        public void Vector4TransformVector2Test1()
        {
            Vector2 v = new Vector2(1.0, 2.0);
            Matrix4x4 m = new Matrix4x4();
            Vector4 expected = new Vector4(0, 0, 0, 0);

            Vector4 actual = Vector4.Transform(v, m);
            Assert.True(MathHelper.Equal(expected, actual), "Vector4.Transform did not return the expected value.");
        }

        // A test for Transform (Vector2, Matrix4x4)
        // Transform vector2 with identity matrix
        [Fact]
        public void Vector4TransformVector2Test2()
        {
            Vector2 v = new Vector2(1.0, 2.0);
            Matrix4x4 m = Matrix4x4.Identity;
            Vector4 expected = new Vector4(1.0, 2.0, 0, 1.0);

            Vector4 actual = Vector4.Transform(v, m);
            Assert.True(MathHelper.Equal(expected, actual), "Vector4.Transform did not return the expected value.");
        }

        // A test for Transform (Vector2, Quaternion)
        [Fact]
        public void Vector4TransformVector2QuatanionTest()
        {
            Vector2 v = new Vector2(1.0, 2.0);

            Matrix4x4 m =
                Matrix4x4.CreateRotationX(MathHelper.ToRadians(30.0)) *
                Matrix4x4.CreateRotationY(MathHelper.ToRadians(30.0)) *
                Matrix4x4.CreateRotationZ(MathHelper.ToRadians(30.0));

            Quaternion q = Quaternion.CreateFromRotationMatrix(m);

            Vector4 expected = Vector4.Transform(v, m);
            Vector4 actual;

            actual = Vector4.Transform(v, q);
            Assert.True(MathHelper.Equal(expected, actual), "Vector4.Transform did not return the expected value.");
        }

        // A test for Transform (Vector3, Quaternion)
        [Fact]
        public void Vector4TransformVector3Quaternion()
        {
            Vector3 v = new Vector3(1.0, 2.0, 3.0);

            Matrix4x4 m =
                Matrix4x4.CreateRotationX(MathHelper.ToRadians(30.0)) *
                Matrix4x4.CreateRotationY(MathHelper.ToRadians(30.0)) *
                Matrix4x4.CreateRotationZ(MathHelper.ToRadians(30.0));
            Quaternion q = Quaternion.CreateFromRotationMatrix(m);

            Vector4 expected = Vector4.Transform(v, m);
            Vector4 actual;

            actual = Vector4.Transform(v, q);
            Assert.True(MathHelper.Equal(expected, actual), "vector4.Transform did not return the expected value.");
        }

        // A test for Transform (Vector4, Quaternion)
        [Fact]
        public void Vector4TransformVector4QuaternionTest()
        {
            Vector4 v = new Vector4(1.0, 2.0, 3.0, 0.0);

            Matrix4x4 m =
                Matrix4x4.CreateRotationX(MathHelper.ToRadians(30.0)) *
                Matrix4x4.CreateRotationY(MathHelper.ToRadians(30.0)) *
                Matrix4x4.CreateRotationZ(MathHelper.ToRadians(30.0));
            Quaternion q = Quaternion.CreateFromRotationMatrix(m);

            Vector4 expected = Vector4.Transform(v, m);
            Vector4 actual;

            actual = Vector4.Transform(v, q);
            Assert.True(MathHelper.Equal(expected, actual), "Vector4.Transform did not return the expected value.");

            // 
            v.W = 1.0;
            expected.W = 1.0;
            actual = Vector4.Transform(v, q);
            Assert.True(MathHelper.Equal(expected, actual), "Vector4.Transform did not return the expected value.");
        }

        // A test for Transform (Vector4, Quaternion)
        // Transform vector4 with zero quaternion
        [Fact]
        public void Vector4TransformVector4QuaternionTest1()
        {
            Vector4 v = new Vector4(1.0, 2.0, 3.0, 0.0);
            Quaternion q = new Quaternion();
            Vector4 expected = v;

            Vector4 actual = Vector4.Transform(v, q);
            Assert.True(MathHelper.Equal(expected, actual), "Vector4.Transform did not return the expected value.");
        }

        // A test for Transform (Vector4, Quaternion)
        // Transform vector4 with identity matrix
        [Fact]
        public void Vector4TransformVector4QuaternionTest2()
        {
            Vector4 v = new Vector4(1.0, 2.0, 3.0, 0.0);
            Quaternion q = Quaternion.Identity;
            Vector4 expected = new Vector4(1.0, 2.0, 3.0, 0.0);

            Vector4 actual = Vector4.Transform(v, q);
            Assert.True(MathHelper.Equal(expected, actual), "Vector4.Transform did not return the expected value.");
        }

        // A test for Transform (Vector3, Quaternion)
        // Transform Vector3 test
        [Fact]
        public void Vector4TransformVector3QuaternionTest()
        {
            Vector3 v = new Vector3(1.0, 2.0, 3.0);

            Matrix4x4 m =
                Matrix4x4.CreateRotationX(MathHelper.ToRadians(30.0)) *
                Matrix4x4.CreateRotationY(MathHelper.ToRadians(30.0)) *
                Matrix4x4.CreateRotationZ(MathHelper.ToRadians(30.0));
            Quaternion q = Quaternion.CreateFromRotationMatrix(m);

            Vector4 expected = Vector4.Transform(v, m);
            Vector4 actual = Vector4.Transform(v, q);
            Assert.True(MathHelper.Equal(expected, actual), "Vector4.Transform did not return the expected value.");
        }

        // A test for Transform (Vector3, Quaternion)
        // Transform vector3 with zero quaternion
        [Fact]
        public void Vector4TransformVector3QuaternionTest1()
        {
            Vector3 v = new Vector3(1.0, 2.0, 3.0);
            Quaternion q = new Quaternion();
            Vector4 expected = new Vector4(v, 1.0);

            Vector4 actual = Vector4.Transform(v, q);
            Assert.True(MathHelper.Equal(expected, actual), "Vector4.Transform did not return the expected value.");
        }

        // A test for Transform (Vector3, Quaternion)
        // Transform vector3 with identity quaternion
        [Fact]
        public void Vector4TransformVector3QuaternionTest2()
        {
            Vector3 v = new Vector3(1.0, 2.0, 3.0);
            Quaternion q = Quaternion.Identity;
            Vector4 expected = new Vector4(1.0, 2.0, 3.0, 1.0);

            Vector4 actual = Vector4.Transform(v, q);
            Assert.True(MathHelper.Equal(expected, actual), "Vector4.Transform did not return the expected value.");
        }

        // A test for Transform (Vector2, Quaternion)
        // Transform Vector2 by quaternion test
        [Fact]
        public void Vector4TransformVector2QuaternionTest()
        {
            Vector2 v = new Vector2(1.0, 2.0);

            Matrix4x4 m =
                Matrix4x4.CreateRotationX(MathHelper.ToRadians(30.0)) *
                Matrix4x4.CreateRotationY(MathHelper.ToRadians(30.0)) *
                Matrix4x4.CreateRotationZ(MathHelper.ToRadians(30.0));
            Quaternion q = Quaternion.CreateFromRotationMatrix(m);

            Vector4 expected = Vector4.Transform(v, m);
            Vector4 actual = Vector4.Transform(v, q);
            Assert.True(MathHelper.Equal(expected, actual), "Vector4.Transform did not return the expected value.");
        }

        // A test for Transform (Vector2, Quaternion)
        // Transform Vector2 with zero quaternion
        [Fact]
        public void Vector4TransformVector2QuaternionTest1()
        {
            Vector2 v = new Vector2(1.0, 2.0);
            Quaternion q = new Quaternion();
            Vector4 expected = new Vector4(1.0, 2.0, 0, 1.0);

            Vector4 actual = Vector4.Transform(v, q);
            Assert.True(MathHelper.Equal(expected, actual), "Vector4.Transform did not return the expected value.");
        }

        // A test for Transform (Vector2, Matrix4x4)
        // Transform vector2 with identity Quaternion
        [Fact]
        public void Vector4TransformVector2QuaternionTest2()
        {
            Vector2 v = new Vector2(1.0, 2.0);
            Quaternion q = Quaternion.Identity;
            Vector4 expected = new Vector4(1.0, 2.0, 0, 1.0);

            Vector4 actual = Vector4.Transform(v, q);
            Assert.True(MathHelper.Equal(expected, actual), "Vector4.Transform did not return the expected value.");
        }

        // A test for Normalize (Vector4)
        [Fact]
        public void Vector4NormalizeTest()
        {
            Vector4 a = new Vector4(1.0, 2.0, 3.0, 4.0);

            Vector4 expected = new Vector4(
                0.1825741858350553711523232609336,
                0.3651483716701107423046465218672,
                0.5477225575051661134569697828008,
                0.7302967433402214846092930437344);
            Vector4 actual;

            actual = Vector4.Normalize(a);
            Assert.True(MathHelper.Equal(expected, actual), "Vector4.Normalize did not return the expected value.");
        }

        // A test for Normalize (Vector4)
        // Normalize vector of length one
        [Fact]
        public void Vector4NormalizeTest1()
        {
            Vector4 a = new Vector4(1.0, 0.0, 0.0, 0.0);

            Vector4 expected = new Vector4(1.0, 0.0, 0.0, 0.0);
            Vector4 actual = Vector4.Normalize(a);
            Assert.True(MathHelper.Equal(expected, actual), "Vector4.Normalize did not return the expected value.");
        }

        // A test for Normalize (Vector4)
        // Normalize vector of length zero
        [Fact]
        public void Vector4NormalizeTest2()
        {
            Vector4 a = new Vector4(0.0, 0.0, 0.0, 0.0);

            Vector4 expected = new Vector4(0.0, 0.0, 0.0, 0.0);
            Vector4 actual = Vector4.Normalize(a);
            Assert.True(double.IsNaN(actual.X) && double.IsNaN(actual.Y) && double.IsNaN(actual.Z) && double.IsNaN(actual.W), "Vector4.Normalize did not return the expected value.");
        }

        // A test for operator - (Vector4)
        [Fact]
        public void Vector4UnaryNegationTest()
        {
            Vector4 a = new Vector4(1.0, 2.0, 3.0, 4.0);

            Vector4 expected = new Vector4(-1.0, -2.0, -3.0, -4.0);
            Vector4 actual;

            actual = -a;

            Assert.True(MathHelper.Equal(expected, actual), "Vector4.operator - did not return the expected value.");
        }

        // A test for operator - (Vector4, Vector4)
        [Fact]
        public void Vector4SubtractionTest()
        {
            Vector4 a = new Vector4(1.0, 6.0, 3.0, 4.0);
            Vector4 b = new Vector4(5.0, 2.0, 3.0, 9.0);

            Vector4 expected = new Vector4(-4.0, 4.0, 0.0, -5.0);
            Vector4 actual;

            actual = a - b;

            Assert.True(MathHelper.Equal(expected, actual), "Vector4.operator - did not return the expected value.");
        }

        // A test for operator * (Vector4, double)
        [Fact]
        public void Vector4MultiplyOperatorTest()
        {
            Vector4 a = new Vector4(1.0, 2.0, 3.0, 4.0);

            const double factor = 2.0;

            Vector4 expected = new Vector4(2.0, 4.0, 6.0, 8.0);
            Vector4 actual;

            actual = a * factor;
            Assert.True(MathHelper.Equal(expected, actual), "Vector4.operator * did not return the expected value.");
        }

        // A test for operator * (double, Vector4)
        [Fact]
        public void Vector4MultiplyOperatorTest2()
        {
            Vector4 a = new Vector4(1.0, 2.0, 3.0, 4.0);

            const double factor = 2.0;
            Vector4 expected = new Vector4(2.0, 4.0, 6.0, 8.0);
            Vector4 actual;

            actual = factor * a;
            Assert.True(MathHelper.Equal(expected, actual), "Vector4.operator * did not return the expected value.");
        }

        // A test for operator * (Vector4, Vector4)
        [Fact]
        public void Vector4MultiplyOperatorTest3()
        {
            Vector4 a = new Vector4(1.0, 2.0, 3.0, 4.0);
            Vector4 b = new Vector4(5.0, 6.0, 7.0, 8.0);

            Vector4 expected = new Vector4(5.0, 12.0, 21.0, 32.0);
            Vector4 actual;

            actual = a * b;

            Assert.True(MathHelper.Equal(expected, actual), "Vector4.operator * did not return the expected value.");
        }

        // A test for operator / (Vector4, double)
        [Fact]
        public void Vector4DivisionTest()
        {
            Vector4 a = new Vector4(1.0, 2.0, 3.0, 4.0);

            double div = 2.0;

            Vector4 expected = new Vector4(0.5, 1.0, 1.5, 2.0);
            Vector4 actual;

            actual = a / div;

            Assert.True(MathHelper.Equal(expected, actual), "Vector4.operator / did not return the expected value.");
        }

        // A test for operator / (Vector4, Vector4)
        [Fact]
        public void Vector4DivisionTest1()
        {
            Vector4 a = new Vector4(1.0, 6.0, 7.0, 4.0);
            Vector4 b = new Vector4(5.0, 2.0, 3.0, 8.0);

            Vector4 expected = new Vector4(1.0 / 5.0, 6.0 / 2.0, 7.0 / 3.0, 4.0 / 8.0);
            Vector4 actual;

            actual = a / b;

            Assert.True(MathHelper.Equal(expected, actual), "Vector4.operator / did not return the expected value.");
        }

        // A test for operator / (Vector4, Vector4)
        // Divide by zero
        [Fact]
        public void Vector4DivisionTest2()
        {
            Vector4 a = new Vector4(-2.0, 3.0, double.MaxValue, double.NaN);

            double div = 0.0;

            Vector4 actual = a / div;

            Assert.True(double.IsNegativeInfinity(actual.X), "Vector4.operator / did not return the expected value.");
            Assert.True(double.IsPositiveInfinity(actual.Y), "Vector4.operator / did not return the expected value.");
            Assert.True(double.IsPositiveInfinity(actual.Z), "Vector4.operator / did not return the expected value.");
            Assert.True(double.IsNaN(actual.W), "Vector4.operator / did not return the expected value.");
        }

        // A test for operator / (Vector4, Vector4)
        // Divide by zero
        [Fact]
        public void Vector4DivisionTest3()
        {
            Vector4 a = new Vector4(0.047, -3.0, double.NegativeInfinity, double.MinValue);
            Vector4 b = new Vector4();

            Vector4 actual = a / b;

            Assert.True(double.IsPositiveInfinity(actual.X), "Vector4.operator / did not return the expected value.");
            Assert.True(double.IsNegativeInfinity(actual.Y), "Vector4.operator / did not return the expected value.");
            Assert.True(double.IsNegativeInfinity(actual.Z), "Vector4.operator / did not return the expected value.");
            Assert.True(double.IsNegativeInfinity(actual.W), "Vector4.operator / did not return the expected value.");
        }

        // A test for operator + (Vector4, Vector4)
        [Fact]
        public void Vector4AdditionTest()
        {
            Vector4 a = new Vector4(1.0, 2.0, 3.0, 4.0);
            Vector4 b = new Vector4(5.0, 6.0, 7.0, 8.0);

            Vector4 expected = new Vector4(6.0, 8.0, 10.0, 12.0);
            Vector4 actual;

            actual = a + b;

            Assert.True(MathHelper.Equal(expected, actual), "Vector4.operator + did not return the expected value.");
        }

        [Fact]
        public void OperatorAddTest()
        {
            Vector4 v1 = new Vector4(2.5, 2.0, 3.0, 3.3);
            Vector4 v2 = new Vector4(5.5, 4.5, 6.5, 7.5);

            Vector4 v3 = v1 + v2;
            Vector4 v5 = new Vector4(-1.0, 0.0, 0.0, Double.NaN);
            Vector4 v4 = v1 + v5;
            Assert.Equal(8.0, v3.X);
            Assert.Equal(6.5, v3.Y);
            Assert.Equal(9.5, v3.Z);
            Assert.Equal(10.8, v3.W);
            Assert.Equal(1.5, v4.X);
            Assert.Equal(2.0, v4.Y);
            Assert.Equal(3.0, v4.Z);
            Assert.Equal(Double.NaN, v4.W);
        }

        // A test for Vector4 (double, double, double, double)
        [Fact]
        public void Vector4ConstructorTest()
        {
            double x = 1.0;
            double y = 2.0;
            double z = 3.0;
            double w = 4.0;

            Vector4 target = new Vector4(x, y, z, w);

            Assert.True(MathHelper.Equal(target.X, x) && MathHelper.Equal(target.Y, y) && MathHelper.Equal(target.Z, z) && MathHelper.Equal(target.W, w),
                "Vector4 constructor(x,y,z,w) did not return the expected value.");
        }

        // A test for Vector4 (Vector2, double, double)
        [Fact]
        public void Vector4ConstructorTest1()
        {
            Vector2 a = new Vector2(1.0, 2.0);
            double z = 3.0;
            double w = 4.0;

            Vector4 target = new Vector4(a, z, w);
            Assert.True(MathHelper.Equal(target.X, a.X) && MathHelper.Equal(target.Y, a.Y) && MathHelper.Equal(target.Z, z) && MathHelper.Equal(target.W, w),
                "Vector4 constructor(Vector2,z,w) did not return the expected value.");
        }

        // A test for Vector4 (Vector3, double)
        [Fact]
        public void Vector4ConstructorTest2()
        {
            Vector3 a = new Vector3(1.0, 2.0, 3.0);
            double w = 4.0;

            Vector4 target = new Vector4(a, w);

            Assert.True(MathHelper.Equal(target.X, a.X) && MathHelper.Equal(target.Y, a.Y) && MathHelper.Equal(target.Z, a.Z) && MathHelper.Equal(target.W, w),
                "Vector4 constructor(Vector3,w) did not return the expected value.");
        }

        // A test for Vector4 ()
        // Constructor with no parameter
        [Fact]
        public void Vector4ConstructorTest4()
        {
            Vector4 a = new Vector4();

            Assert.Equal(a.X, 0.0);
            Assert.Equal(a.Y, 0.0);
            Assert.Equal(a.Z, 0.0);
            Assert.Equal(a.W, 0.0);
        }

        // A test for Vector4 ()
        // Constructor with special floating values
        [Fact]
        public void Vector4ConstructorTest5()
        {
            Vector4 target = new Vector4(double.NaN, double.MaxValue, double.PositiveInfinity, double.Epsilon);

            Assert.True(double.IsNaN(target.X), "Vector4.constructor (double, double, double, double) did not return the expected value.");
            Assert.True(double.Equals(double.MaxValue, target.Y), "Vector4.constructor (double, double, double, double) did not return the expected value.");
            Assert.True(double.IsPositiveInfinity(target.Z), "Vector4.constructor (double, double, double, double) did not return the expected value.");
            Assert.True(double.Equals(double.Epsilon, target.W), "Vector4.constructor (double, double, double, double) did not return the expected value.");
        }

        // A test for Add (Vector4, Vector4)
        [Fact]
        public void Vector4AddTest()
        {
            Vector4 a = new Vector4(1.0, 2.0, 3.0, 4.0);
            Vector4 b = new Vector4(5.0, 6.0, 7.0, 8.0);

            Vector4 expected = new Vector4(6.0, 8.0, 10.0, 12.0);
            Vector4 actual;

            actual = Vector4.Add(a, b);
            Assert.Equal(expected, actual);
        }

        // A test for Divide (Vector4, double)
        [Fact]
        public void Vector4DivideTest()
        {
            Vector4 a = new Vector4(1.0, 2.0, 3.0, 4.0);
            double div = 2.0;
            Vector4 expected = new Vector4(0.5, 1.0, 1.5, 2.0);
            Vector4 actual;
            actual = Vector4.Divide(a, div);
            Assert.Equal(expected, actual);
        }

        // A test for Divide (Vector4, Vector4)
        [Fact]
        public void Vector4DivideTest1()
        {
            Vector4 a = new Vector4(1.0, 6.0, 7.0, 4.0);
            Vector4 b = new Vector4(5.0, 2.0, 3.0, 8.0);

            Vector4 expected = new Vector4(1.0 / 5.0, 6.0 / 2.0, 7.0 / 3.0, 4.0 / 8.0);
            Vector4 actual;

            actual = Vector4.Divide(a, b);
            Assert.Equal(expected, actual);
        }

        // A test for Equals (object)
        [Fact]
        public void Vector4EqualsTest()
        {
            Vector4 a = new Vector4(1.0, 2.0, 3.0, 4.0);
            Vector4 b = new Vector4(1.0, 2.0, 3.0, 4.0);

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

        // A test for Multiply (double, Vector4)
        [Fact]
        public void Vector4MultiplyTest()
        {
            Vector4 a = new Vector4(1.0, 2.0, 3.0, 4.0);
            const double factor = 2.0;
            Vector4 expected = new Vector4(2.0, 4.0, 6.0, 8.0);
            Vector4 actual = Vector4.Multiply(factor, a);
            Assert.Equal(expected, actual);
        }

        // A test for Multiply (Vector4, double)
        [Fact]
        public void Vector4MultiplyTest2()
        {
            Vector4 a = new Vector4(1.0, 2.0, 3.0, 4.0);
            const double factor = 2.0;
            Vector4 expected = new Vector4(2.0, 4.0, 6.0, 8.0);
            Vector4 actual = Vector4.Multiply(a, factor);
            Assert.Equal(expected, actual);
        }

        // A test for Multiply (Vector4, Vector4)
        [Fact]
        public void Vector4MultiplyTest3()
        {
            Vector4 a = new Vector4(1.0, 2.0, 3.0, 4.0);
            Vector4 b = new Vector4(5.0, 6.0, 7.0, 8.0);

            Vector4 expected = new Vector4(5.0, 12.0, 21.0, 32.0);
            Vector4 actual;

            actual = Vector4.Multiply(a, b);
            Assert.Equal(expected, actual);
        }

        // A test for Negate (Vector4)
        [Fact]
        public void Vector4NegateTest()
        {
            Vector4 a = new Vector4(1.0, 2.0, 3.0, 4.0);

            Vector4 expected = new Vector4(-1.0, -2.0, -3.0, -4.0);
            Vector4 actual;

            actual = Vector4.Negate(a);
            Assert.Equal(expected, actual);
        }

        // A test for operator != (Vector4, Vector4)
        [Fact]
        public void Vector4InequalityTest()
        {
            Vector4 a = new Vector4(1.0, 2.0, 3.0, 4.0);
            Vector4 b = new Vector4(1.0, 2.0, 3.0, 4.0);

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

        // A test for operator == (Vector4, Vector4)
        [Fact]
        public void Vector4EqualityTest()
        {
            Vector4 a = new Vector4(1.0, 2.0, 3.0, 4.0);
            Vector4 b = new Vector4(1.0, 2.0, 3.0, 4.0);

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

        // A test for Subtract (Vector4, Vector4)
        [Fact]
        public void Vector4SubtractTest()
        {
            Vector4 a = new Vector4(1.0, 6.0, 3.0, 4.0);
            Vector4 b = new Vector4(5.0, 2.0, 3.0, 9.0);

            Vector4 expected = new Vector4(-4.0, 4.0, 0.0, -5.0);
            Vector4 actual;

            actual = Vector4.Subtract(a, b);

            Assert.Equal(expected, actual);
        }

        // A test for UnitW
        [Fact]
        public void Vector4UnitWTest()
        {
            Vector4 val = new Vector4(0.0, 0.0, 0.0, 1.0);
            Assert.Equal(val, Vector4.UnitW);
        }

        // A test for UnitX
        [Fact]
        public void Vector4UnitXTest()
        {
            Vector4 val = new Vector4(1.0, 0.0, 0.0, 0.0);
            Assert.Equal(val, Vector4.UnitX);
        }

        // A test for UnitY
        [Fact]
        public void Vector4UnitYTest()
        {
            Vector4 val = new Vector4(0.0, 1.0, 0.0, 0.0);
            Assert.Equal(val, Vector4.UnitY);
        }

        // A test for UnitZ
        [Fact]
        public void Vector4UnitZTest()
        {
            Vector4 val = new Vector4(0.0, 0.0, 1.0, 0.0);
            Assert.Equal(val, Vector4.UnitZ);
        }

        // A test for One
        [Fact]
        public void Vector4OneTest()
        {
            Vector4 val = new Vector4(1.0, 1.0, 1.0, 1.0);
            Assert.Equal(val, Vector4.One);
        }

        // A test for Zero
        [Fact]
        public void Vector4ZeroTest()
        {
            Vector4 val = new Vector4(0.0, 0.0, 0.0, 0.0);
            Assert.Equal(val, Vector4.Zero);
        }

        // A test for Equals (Vector4)
        [Fact]
        public void Vector4EqualsTest1()
        {
            Vector4 a = new Vector4(1.0, 2.0, 3.0, 4.0);
            Vector4 b = new Vector4(1.0, 2.0, 3.0, 4.0);

            // case 1: compare between same values
            Assert.True(a.Equals(b));

            // case 2: compare between different values
            b.X = 10.0;
            Assert.False(a.Equals(b));
        }

        // A test for Vector4 (double)
        [Fact]
        public void Vector4ConstructorTest6()
        {
            double value = 1.0;
            Vector4 target = new Vector4(value);

            Vector4 expected = new Vector4(value, value, value, value);
            Assert.Equal(expected, target);

            value = 2.0;
            target = new Vector4(value);
            expected = new Vector4(value, value, value, value);
            Assert.Equal(expected, target);
        }

        // A test for Vector4 comparison involving NaN values
        [Fact]
        public void Vector4EqualsNanTest()
        {
            Vector4 a = new Vector4(double.NaN, 0, 0, 0);
            Vector4 b = new Vector4(0, double.NaN, 0, 0);
            Vector4 c = new Vector4(0, 0, double.NaN, 0);
            Vector4 d = new Vector4(0, 0, 0, double.NaN);

            Assert.False(a == Vector4.Zero);
            Assert.False(b == Vector4.Zero);
            Assert.False(c == Vector4.Zero);
            Assert.False(d == Vector4.Zero);

            Assert.True(a != Vector4.Zero);
            Assert.True(b != Vector4.Zero);
            Assert.True(c != Vector4.Zero);
            Assert.True(d != Vector4.Zero);

            Assert.False(a.Equals(Vector4.Zero));
            Assert.False(b.Equals(Vector4.Zero));
            Assert.False(c.Equals(Vector4.Zero));
            Assert.False(d.Equals(Vector4.Zero));

            // Counterintuitive result - IEEE rules for NaN comparison are weird!
            Assert.False(a.Equals(a));
            Assert.False(b.Equals(b));
            Assert.False(c.Equals(c));
            Assert.False(d.Equals(d));
        }

        [Fact]
        public void Vector4AbsTest()
        {
            Vector4 v1 = new Vector4(-2.5, 2.0, 3.0, 3.3);
            Vector4 v3 = Vector4.Abs(new Vector4(Double.PositiveInfinity, 0.0, Double.NegativeInfinity, Double.NaN));
            Vector4 v = Vector4.Abs(v1);
            Assert.Equal(2.5, v.X);
            Assert.Equal(2.0, v.Y);
            Assert.Equal(3.0, v.Z);
            Assert.Equal(3.3, v.W);
            Assert.Equal(Double.PositiveInfinity, v3.X);
            Assert.Equal(0.0, v3.Y);
            Assert.Equal(Double.PositiveInfinity, v3.Z);
            Assert.Equal(Double.NaN, v3.W);
        }

        [Fact]
        public void Vector4SqrtTest()
        {
            Vector4 v1 = new Vector4(-2.5, 2.0, 3.0, 3.3);
            Vector4 v2 = new Vector4(5.5, 4.5, 6.5, 7.5);
            Assert.Equal(2, (int)Vector4.SquareRoot(v2).X);
            Assert.Equal(2, (int)Vector4.SquareRoot(v2).Y);
            Assert.Equal(2, (int)Vector4.SquareRoot(v2).Z);
            Assert.Equal(2, (int)Vector4.SquareRoot(v2).W);
            Assert.Equal(Double.NaN, Vector4.SquareRoot(v1).X);
        }

        // A test to make sure these types are blittable directly into GPU buffer memory layouts
        [Fact]
        public unsafe void Vector4SizeofTest()
        {
            Assert.Equal(16*2, sizeof(Vector4));
            Assert.Equal(32*2, sizeof(Vector4_2x));
            Assert.Equal(20*2, sizeof(Vector4PlusFloat));
            Assert.Equal(40*2, sizeof(Vector4PlusFloat_2x));
        }

        [StructLayout(LayoutKind.Sequential)]
        struct Vector4_2x
        {
            private Vector4 _a;
            private Vector4 _b;
        }

        [StructLayout(LayoutKind.Sequential)]
        struct Vector4PlusFloat
        {
            private Vector4 _v;
            private double _f;
        }

        [StructLayout(LayoutKind.Sequential)]
        struct Vector4PlusFloat_2x
        {
            private Vector4PlusFloat _a;
            private Vector4PlusFloat _b;
        }

        [Fact]
        public void SetFieldsTest()
        {
            Vector4 v3 = new Vector4(4, 5, 6, 7);
            v3.X = 1.0;
            v3.Y = 2.0;
            v3.Z = 3.0;
            v3.W = 4.0;
            Assert.Equal(1.0, v3.X);
            Assert.Equal(2.0, v3.Y);
            Assert.Equal(3.0, v3.Z);
            Assert.Equal(4.0, v3.W);
            Vector4 v4 = v3;
            v4.Y = 0.5;
            v4.Z = 2.2;
            v4.W = 3.5;
            Assert.Equal(1.0, v4.X);
            Assert.Equal(0.5, v4.Y);
            Assert.Equal(2.2, v4.Z);
            Assert.Equal(3.5, v4.W);
            Assert.Equal(2.0, v3.Y);
        }

        [Fact]
        public void EmbeddedVectorSetFields()
        {
            EmbeddedVectorObject evo = new EmbeddedVectorObject();
            evo.FieldVector.X = 5.0;
            evo.FieldVector.Y = 5.0;
            evo.FieldVector.Z = 5.0;
            evo.FieldVector.W = 5.0;
            Assert.Equal(5.0, evo.FieldVector.X);
            Assert.Equal(5.0, evo.FieldVector.Y);
            Assert.Equal(5.0, evo.FieldVector.Z);
            Assert.Equal(5.0, evo.FieldVector.W);
        }

        [Fact]
        public void DeeplyEmbeddedObjectTest()
        {
            DeeplyEmbeddedClass obj = new DeeplyEmbeddedClass();
            obj.L0.L1.L2.L3.L4.L5.L6.L7.EmbeddedVector.X = 5;
            Assert.Equal(5, obj.RootEmbeddedObject.X);
            Assert.Equal(5, obj.RootEmbeddedObject.Y);
            Assert.Equal(1, obj.RootEmbeddedObject.Z);
            Assert.Equal(-5, obj.RootEmbeddedObject.W);
            obj.L0.L1.L2.L3.L4.L5.L6.L7.EmbeddedVector = new Vector4(1, 2, 3, 4);
            Assert.Equal(1, obj.RootEmbeddedObject.X);
            Assert.Equal(2, obj.RootEmbeddedObject.Y);
            Assert.Equal(3, obj.RootEmbeddedObject.Z);
            Assert.Equal(4, obj.RootEmbeddedObject.W);
        }

        [Fact]
        public void DeeplyEmbeddedStructTest()
        {
            DeeplyEmbeddedStruct obj = DeeplyEmbeddedStruct.Create();
            obj.L0.L1.L2.L3.L4.L5.L6.L7.EmbeddedVector.X = 5;
            Assert.Equal(5, obj.RootEmbeddedObject.X);
            Assert.Equal(5, obj.RootEmbeddedObject.Y);
            Assert.Equal(1, obj.RootEmbeddedObject.Z);
            Assert.Equal(-5, obj.RootEmbeddedObject.W);
            obj.L0.L1.L2.L3.L4.L5.L6.L7.EmbeddedVector = new Vector4(1, 2, 3, 4);
            Assert.Equal(1, obj.RootEmbeddedObject.X);
            Assert.Equal(2, obj.RootEmbeddedObject.Y);
            Assert.Equal(3, obj.RootEmbeddedObject.Z);
            Assert.Equal(4, obj.RootEmbeddedObject.W);
        }

        private class EmbeddedVectorObject
        {
            public Vector4 FieldVector;
        }

        private class DeeplyEmbeddedClass
        {
            public readonly Level0 L0 = new Level0();
            public Vector4 RootEmbeddedObject { get { return L0.L1.L2.L3.L4.L5.L6.L7.EmbeddedVector; } }
            public class Level0
            {
                public readonly Level1 L1 = new Level1();
                public class Level1
                {
                    public readonly Level2 L2 = new Level2();
                    public class Level2
                    {
                        public readonly Level3 L3 = new Level3();
                        public class Level3
                        {
                            public readonly Level4 L4 = new Level4();
                            public class Level4
                            {
                                public readonly Level5 L5 = new Level5();
                                public class Level5
                                {
                                    public readonly Level6 L6 = new Level6();
                                    public class Level6
                                    {
                                        public readonly Level7 L7 = new Level7();
                                        public class Level7
                                        {
                                            public Vector4 EmbeddedVector = new Vector4(1, 5, 1, -5);
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }
        }

        // Contrived test for strangely-sized and shaped embedded structures, with unused buffer fields.
#pragma warning disable 0169
        private struct DeeplyEmbeddedStruct
        {
            public static DeeplyEmbeddedStruct Create()
            {
                var obj = new DeeplyEmbeddedStruct();
                obj.L0 = new Level0();
                obj.L0.L1 = new Level0.Level1();
                obj.L0.L1.L2 = new Level0.Level1.Level2();
                obj.L0.L1.L2.L3 = new Level0.Level1.Level2.Level3();
                obj.L0.L1.L2.L3.L4 = new Level0.Level1.Level2.Level3.Level4();
                obj.L0.L1.L2.L3.L4.L5 = new Level0.Level1.Level2.Level3.Level4.Level5();
                obj.L0.L1.L2.L3.L4.L5.L6 = new Level0.Level1.Level2.Level3.Level4.Level5.Level6();
                obj.L0.L1.L2.L3.L4.L5.L6.L7 = new Level0.Level1.Level2.Level3.Level4.Level5.Level6.Level7();
                obj.L0.L1.L2.L3.L4.L5.L6.L7.EmbeddedVector = new Vector4(1, 5, 1, -5);

                return obj;
            }

            public Level0 L0;
            public Vector4 RootEmbeddedObject { get { return L0.L1.L2.L3.L4.L5.L6.L7.EmbeddedVector; } }
            public struct Level0
            {
                private double _buffer0, _buffer1;
                public Level1 L1;
                private double _buffer2;
                public struct Level1
                {
                    private double _buffer0, _buffer1;
                    public Level2 L2;
                    private byte _buffer2;
                    public struct Level2
                    {
                        public Level3 L3;
                        private double _buffer0;
                        private byte _buffer1;
                        public struct Level3
                        {
                            public Level4 L4;
                            public struct Level4
                            {
                                private double _buffer0;
                                public Level5 L5;
                                private long _buffer1;
                                private byte _buffer2;
                                private double _buffer3;
                                public struct Level5
                                {
                                    private byte _buffer0;
                                    public Level6 L6;
                                    public struct Level6
                                    {
                                        private byte _buffer0;
                                        public Level7 L7;
                                        private byte _buffer1, _buffer2;
                                        public struct Level7
                                        {
                                            public Vector4 EmbeddedVector;
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }
        }
#pragma warning restore 0169
    }
}
