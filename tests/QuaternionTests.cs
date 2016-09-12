// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using System.Globalization;
using System.Runtime.InteropServices;
using Xunit;

namespace System.Numerics.Tests
{
    public class QuaternionTests
    {
        // A test for Dot (Quaternion, Quaternion)
        [Fact]
        public void QuaternionDotTest()
        {
            Quaternion a = new Quaternion(1.0, 2.0, 3.0, 4.0);
            Quaternion b = new Quaternion(5.0, 6.0, 7.0, 8.0);

            double expected = 70.0;
            double actual;

            actual = Quaternion.Dot(a, b);
            Assert.True(MathHelper.Equal(expected, actual), "Quaternion.Dot did not return the expected value.");
        }

        // A test for Length ()
        [Fact]
        public void QuaternionLengthTest()
        {
            Vector3 v = new Vector3(1.0, 2.0, 3.0);

            double w = 4.0;

            Quaternion target = new Quaternion(v, w);

            double expected = 5.477226;
            double actual;

            actual = target.Length();

            Assert.True(MathHelper.Equal(expected, actual), "Quaternion.Length did not return the expected value.");
        }

        // A test for LengthSquared ()
        [Fact]
        public void QuaternionLengthSquaredTest()
        {
            Vector3 v = new Vector3(1.0, 2.0, 3.0);
            double w = 4.0;

            Quaternion target = new Quaternion(v, w);

            double expected = 30.0;
            double actual;

            actual = target.LengthSquared();

            Assert.True(MathHelper.Equal(expected, actual), "Quaternion.LengthSquared did not return the expected value.");
        }

        // A test for Lerp (Quaternion, Quaternion, double)
        [Fact]
        public void QuaternionLerpTest()
        {
            Vector3 axis = Vector3.Normalize(new Vector3(1.0, 2.0, 3.0));
            Quaternion a = Quaternion.CreateFromAxisAngle(axis, MathHelper.ToRadians(10.0));
            Quaternion b = Quaternion.CreateFromAxisAngle(axis, MathHelper.ToRadians(30.0));

            double t = 0.5;

            Quaternion expected = Quaternion.CreateFromAxisAngle(axis, MathHelper.ToRadians(20.0));
            Quaternion actual;

            actual = Quaternion.Lerp(a, b, t);
            Assert.True(MathHelper.Equal(expected, actual), "Quaternion.Lerp did not return the expected value.");

            // Case a and b are same.
            expected = a;
            actual = Quaternion.Lerp(a, a, t);
            Assert.True(MathHelper.Equal(expected, actual), "Quaternion.Lerp did not return the expected value.");
        }

        // A test for Lerp (Quaternion, Quaternion, double)
        // Lerp test when t = 0
        [Fact]
        public void QuaternionLerpTest1()
        {
            Vector3 axis = Vector3.Normalize(new Vector3(1.0, 2.0, 3.0));
            Quaternion a = Quaternion.CreateFromAxisAngle(axis, MathHelper.ToRadians(10.0));
            Quaternion b = Quaternion.CreateFromAxisAngle(axis, MathHelper.ToRadians(30.0));

            double t = 0.0;

            Quaternion expected = new Quaternion(a.X, a.Y, a.Z, a.W);
            Quaternion actual = Quaternion.Lerp(a, b, t);
            Assert.True(MathHelper.Equal(expected, actual), "Quaternion.Lerp did not return the expected value.");
        }

        // A test for Lerp (Quaternion, Quaternion, double)
        // Lerp test when t = 1
        [Fact]
        public void QuaternionLerpTest2()
        {
            Vector3 axis = Vector3.Normalize(new Vector3(1.0, 2.0, 3.0));
            Quaternion a = Quaternion.CreateFromAxisAngle(axis, MathHelper.ToRadians(10.0));
            Quaternion b = Quaternion.CreateFromAxisAngle(axis, MathHelper.ToRadians(30.0));

            double t = 1.0;

            Quaternion expected = new Quaternion(b.X, b.Y, b.Z, b.W);
            Quaternion actual = Quaternion.Lerp(a, b, t);
            Assert.True(MathHelper.Equal(expected, actual), "Quaternion.Lerp did not return the expected value.");
        }

        // A test for Lerp (Quaternion, Quaternion, double)
        // Lerp test when the two quaternions are more than 90 degree (dot product <0)
        [Fact]
        public void QuaternionLerpTest3()
        {
            Vector3 axis = Vector3.Normalize(new Vector3(1.0, 2.0, 3.0));
            Quaternion a = Quaternion.CreateFromAxisAngle(axis, MathHelper.ToRadians(10.0));
            Quaternion b = Quaternion.Negate(a);

            double t = 1.0;

            Quaternion actual = Quaternion.Lerp(a, b, t);
            // Note that in quaternion world, Q == -Q. In the case of quaternions dot product is zero, 
            // one of the quaternion will be flipped to compute the shortest distance. When t = 1, we
            // expect the result to be the same as quaternion b but flipped.
            Assert.True(actual == a, "Quaternion.Lerp did not return the expected value.");
        }

        // A test for Conjugate(Quaternion)
        [Fact]
        public void QuaternionConjugateTest1()
        {
            Quaternion a = new Quaternion(1, 2, 3, 4);

            Quaternion expected = new Quaternion(-1, -2, -3, 4);
            Quaternion actual;

            actual = Quaternion.Conjugate(a);
            Assert.True(MathHelper.Equal(expected, actual), "Quaternion.Conjugate did not return the expected value.");
        }

        // A test for Normalize (Quaternion)
        [Fact]
        public void QuaternionNormalizeTest()
        {
            Quaternion a = new Quaternion(1.0, 2.0, 3.0, 4.0);

            Quaternion expected = new Quaternion(0.182574168, 0.365148336, 0.5477225, 0.7302967);
            Quaternion actual;

            actual = Quaternion.Normalize(a);
            Assert.True(MathHelper.Equal(expected, actual), "Quaternion.Normalize did not return the expected value.");
        }

        // A test for Normalize (Quaternion)
        // Normalize zero length quaternion
        [Fact]
        public void QuaternionNormalizeTest1()
        {
            Quaternion a = new Quaternion(0.0, 0.0, -0.0, 0.0);

            Quaternion actual = Quaternion.Normalize(a);
            Assert.True(double.IsNaN(actual.X) && double.IsNaN(actual.Y) && double.IsNaN(actual.Z) && double.IsNaN(actual.W)
                , "Quaternion.Normalize did not return the expected value.");
        }

        // A test for Concatenate(Quaternion, Quaternion)
        [Fact]
        public void QuaternionConcatenateTest1()
        {
            Quaternion b = new Quaternion(1.0, 2.0, 3.0, 4.0);
            Quaternion a = new Quaternion(5.0, 6.0, 7.0, 8.0);

            Quaternion expected = new Quaternion(24.0, 48.0, 48.0, -6.0);
            Quaternion actual;

            actual = Quaternion.Concatenate(a, b);
            Assert.True(MathHelper.Equal(expected, actual), "Quaternion.Concatenate did not return the expected value.");
        }

        // A test for operator - (Quaternion, Quaternion)
        [Fact]
        public void QuaternionSubtractionTest()
        {
            Quaternion a = new Quaternion(1.0, 6.0, 7.0, 4.0);
            Quaternion b = new Quaternion(5.0, 2.0, 3.0, 8.0);

            Quaternion expected = new Quaternion(-4.0, 4.0, 4.0, -4.0);
            Quaternion actual;

            actual = a - b;

            Assert.True(MathHelper.Equal(expected, actual), "Quaternion.operator - did not return the expected value.");
        }

        // A test for operator * (Quaternion, double)
        [Fact]
        public void QuaternionMultiplyTest()
        {
            Quaternion a = new Quaternion(1.0, 2.0, 3.0, 4.0);
            double factor = 0.5;

            Quaternion expected = new Quaternion(0.5, 1.0, 1.5, 2.0);
            Quaternion actual;

            actual = a * factor;

            Assert.True(MathHelper.Equal(expected, actual), "Quaternion.operator * did not return the expected value.");
        }

        // A test for operator * (Quaternion, Quaternion)
        [Fact]
        public void QuaternionMultiplyTest1()
        {
            Quaternion a = new Quaternion(1.0, 2.0, 3.0, 4.0);
            Quaternion b = new Quaternion(5.0, 6.0, 7.0, 8.0);

            Quaternion expected = new Quaternion(24.0, 48.0, 48.0, -6.0);
            Quaternion actual;

            actual = a * b;

            Assert.True(MathHelper.Equal(expected, actual), "Quaternion.operator * did not return the expected value.");
        }

        // A test for operator / (Quaternion, Quaternion)
        [Fact]
        public void QuaternionDivisionTest1()
        {
            Quaternion a = new Quaternion(1.0, 2.0, 3.0, 4.0);
            Quaternion b = new Quaternion(5.0, 6.0, 7.0, 8.0);

            Quaternion expected = new Quaternion(-0.045977015, -0.09195402, -7.450581E-9, 0.402298868);
            Quaternion actual;

            actual = a / b;

            Assert.True(MathHelper.Equal(expected, actual), "Quaternion.operator / did not return the expected value.");
        }

        // A test for operator + (Quaternion, Quaternion)
        [Fact]
        public void QuaternionAdditionTest()
        {
            Quaternion a = new Quaternion(1.0, 2.0, 3.0, 4.0);
            Quaternion b = new Quaternion(5.0, 6.0, 7.0, 8.0);

            Quaternion expected = new Quaternion(6.0, 8.0, 10.0, 12.0);
            Quaternion actual;

            actual = a + b;

            Assert.True(MathHelper.Equal(expected, actual), "Quaternion.operator + did not return the expected value.");
        }

        // A test for Quaternion (double, double, double, double)
        [Fact]
        public void QuaternionConstructorTest()
        {
            double x = 1.0;
            double y = 2.0;
            double z = 3.0;
            double w = 4.0;

            Quaternion target = new Quaternion(x, y, z, w);

            Assert.True(MathHelper.Equal(target.X, x) && MathHelper.Equal(target.Y, y) && MathHelper.Equal(target.Z, z) && MathHelper.Equal(target.W, w),
                "Quaternion.constructor (x,y,z,w) did not return the expected value.");
        }

        // A test for Quaternion (Vector3, double)
        [Fact]
        public void QuaternionConstructorTest1()
        {
            Vector3 v = new Vector3(1.0, 2.0, 3.0);
            double w = 4.0;

            Quaternion target = new Quaternion(v, w);
            Assert.True(MathHelper.Equal(target.X, v.X) && MathHelper.Equal(target.Y, v.Y) && MathHelper.Equal(target.Z, v.Z) && MathHelper.Equal(target.W, w),
                "Quaternion.constructor (Vector3,w) did not return the expected value.");
        }

        // A test for CreateFromAxisAngle (Vector3, double)
        [Fact]
        public void QuaternionCreateFromAxisAngleTest()
        {
            Vector3 axis = Vector3.Normalize(new Vector3(1.0, 2.0, 3.0));
            double angle = MathHelper.ToRadians(30.0);

            Quaternion expected = new Quaternion(0.0691723, 0.1383446, 0.207516879, 0.9659258);
            Quaternion actual;

            actual = Quaternion.CreateFromAxisAngle(axis, angle);
            Assert.True(MathHelper.Equal(expected, actual), "Quaternion.CreateFromAxisAngle did not return the expected value.");
        }

        // A test for CreateFromAxisAngle (Vector3, double)
        // CreateFromAxisAngle of zero vector
        [Fact]
        public void QuaternionCreateFromAxisAngleTest1()
        {
            Vector3 axis = new Vector3();
            double angle = MathHelper.ToRadians(-30.0);

            double cos = (double)System.Math.Cos(angle / 2.0);
            Quaternion actual = Quaternion.CreateFromAxisAngle(axis, angle);

            Assert.True(actual.X == 0.0 && actual.Y == 0.0 && actual.Z == 0.0 && MathHelper.Equal(cos, actual.W)
                , "Quaternion.CreateFromAxisAngle did not return the expected value.");
        }

        // A test for CreateFromAxisAngle (Vector3, double)
        // CreateFromAxisAngle of angle = 30 && 750
        [Fact]
        public void QuaternionCreateFromAxisAngleTest2()
        {
            Vector3 axis = new Vector3(1, 0, 0);
            double angle1 = MathHelper.ToRadians(30.0);
            double angle2 = MathHelper.ToRadians(750.0);

            Quaternion actual1 = Quaternion.CreateFromAxisAngle(axis, angle1);
            Quaternion actual2 = Quaternion.CreateFromAxisAngle(axis, angle2);
            Assert.True(MathHelper.Equal(actual1, actual2), "Quaternion.CreateFromAxisAngle did not return the expected value.");
        }

        // A test for CreateFromAxisAngle (Vector3, double)
        // CreateFromAxisAngle of angle = 30 && 390
        [Fact]
        public void QuaternionCreateFromAxisAngleTest3()
        {
            Vector3 axis = new Vector3(1, 0, 0);
            double angle1 = MathHelper.ToRadians(30.0);
            double angle2 = MathHelper.ToRadians(390.0);

            Quaternion actual1 = Quaternion.CreateFromAxisAngle(axis, angle1);
            Quaternion actual2 = Quaternion.CreateFromAxisAngle(axis, angle2);
            actual1.X = -actual1.X;
            actual1.W = -actual1.W;

            Assert.True(MathHelper.Equal(actual1, actual2), "Quaternion.CreateFromAxisAngle did not return the expected value.");
        }

        [Fact]
        public void QuaternionCreateFromYawPitchRollTest1()
        {
            double yawAngle = MathHelper.ToRadians(30.0);
            double pitchAngle = MathHelper.ToRadians(40.0);
            double rollAngle = MathHelper.ToRadians(50.0);

            Quaternion yaw = Quaternion.CreateFromAxisAngle(Vector3.UnitY, yawAngle);
            Quaternion pitch = Quaternion.CreateFromAxisAngle(Vector3.UnitX, pitchAngle);
            Quaternion roll = Quaternion.CreateFromAxisAngle(Vector3.UnitZ, rollAngle);

            Quaternion expected = yaw * pitch * roll;
            Quaternion actual = Quaternion.CreateFromYawPitchRoll(yawAngle, pitchAngle, rollAngle);
            Assert.True(MathHelper.Equal(expected, actual));
        }

        // Covers more numeric rigions
        [Fact]
        public void QuaternionCreateFromYawPitchRollTest2()
        {
            const double step = 35.0;

            for (double yawAngle = -720.0; yawAngle <= 720.0; yawAngle += step)
            {
                for (double pitchAngle = -720.0; pitchAngle <= 720.0; pitchAngle += step)
                {
                    for (double rollAngle = -720.0; rollAngle <= 720.0; rollAngle += step)
                    {
                        double yawRad = MathHelper.ToRadians(yawAngle);
                        double pitchRad = MathHelper.ToRadians(pitchAngle);
                        double rollRad = MathHelper.ToRadians(rollAngle);

                        Quaternion yaw = Quaternion.CreateFromAxisAngle(Vector3.UnitY, yawRad);
                        Quaternion pitch = Quaternion.CreateFromAxisAngle(Vector3.UnitX, pitchRad);
                        Quaternion roll = Quaternion.CreateFromAxisAngle(Vector3.UnitZ, rollRad);

                        Quaternion expected = yaw * pitch * roll;
                        Quaternion actual = Quaternion.CreateFromYawPitchRoll(yawRad, pitchRad, rollRad);
                        Assert.True(MathHelper.Equal(expected, actual), String.Format("Yaw:{0} Pitch:{1} Roll:{2}", yawAngle, pitchAngle, rollAngle));
                    }
                }
            }
        }

        // A test for Slerp (Quaternion, Quaternion, double)
        [Fact]
        public void QuaternionSlerpTest()
        {
            Vector3 axis = Vector3.Normalize(new Vector3(1.0, 2.0, 3.0));
            Quaternion a = Quaternion.CreateFromAxisAngle(axis, MathHelper.ToRadians(10.0));
            Quaternion b = Quaternion.CreateFromAxisAngle(axis, MathHelper.ToRadians(30.0));

            double t = 0.5;

            Quaternion expected = Quaternion.CreateFromAxisAngle(axis, MathHelper.ToRadians(20.0));
            Quaternion actual;

            actual = Quaternion.Slerp(a, b, t);
            Assert.True(MathHelper.Equal(expected, actual), "Quaternion.Slerp did not return the expected value.");

            // Case a and b are same.
            expected = a;
            actual = Quaternion.Slerp(a, a, t);
            Assert.True(MathHelper.Equal(expected, actual), "Quaternion.Slerp did not return the expected value.");
        }

        // A test for Slerp (Quaternion, Quaternion, double)
        // Slerp test where t = 0
        [Fact]
        public void QuaternionSlerpTest1()
        {
            Vector3 axis = Vector3.Normalize(new Vector3(1.0, 2.0, 3.0));
            Quaternion a = Quaternion.CreateFromAxisAngle(axis, MathHelper.ToRadians(10.0));
            Quaternion b = Quaternion.CreateFromAxisAngle(axis, MathHelper.ToRadians(30.0));

            double t = 0.0;

            Quaternion expected = new Quaternion(a.X, a.Y, a.Z, a.W);
            Quaternion actual = Quaternion.Slerp(a, b, t);
            Assert.True(MathHelper.Equal(expected, actual), "Quaternion.Slerp did not return the expected value.");
        }

        // A test for Slerp (Quaternion, Quaternion, double)
        // Slerp test where t = 1
        [Fact]
        public void QuaternionSlerpTest2()
        {
            Vector3 axis = Vector3.Normalize(new Vector3(1.0, 2.0, 3.0));
            Quaternion a = Quaternion.CreateFromAxisAngle(axis, MathHelper.ToRadians(10.0));
            Quaternion b = Quaternion.CreateFromAxisAngle(axis, MathHelper.ToRadians(30.0));

            double t = 1.0;

            Quaternion expected = new Quaternion(b.X, b.Y, b.Z, b.W);
            Quaternion actual = Quaternion.Slerp(a, b, t);
            Assert.True(MathHelper.Equal(expected, actual), "Quaternion.Slerp did not return the expected value.");
        }

        // A test for Slerp (Quaternion, Quaternion, double)
        // Slerp test where dot product is < 0
        [Fact]
        public void QuaternionSlerpTest3()
        {
            Vector3 axis = Vector3.Normalize(new Vector3(1.0, 2.0, 3.0));
            Quaternion a = Quaternion.CreateFromAxisAngle(axis, MathHelper.ToRadians(10.0));
            Quaternion b = -a;

            double t = 1.0;

            Quaternion expected = a;
            Quaternion actual = Quaternion.Slerp(a, b, t);
            // Note that in quaternion world, Q == -Q. In the case of quaternions dot product is zero, 
            // one of the quaternion will be flipped to compute the shortest distance. When t = 1, we
            // expect the result to be the same as quaternion b but flipped.
            Assert.True(actual == expected, "Quaternion.Slerp did not return the expected value.");
        }

        // A test for Slerp (Quaternion, Quaternion, double)
        // Slerp test where the quaternion is flipped
        [Fact]
        public void QuaternionSlerpTest4()
        {
            Vector3 axis = Vector3.Normalize(new Vector3(1.0, 2.0, 3.0));
            Quaternion a = Quaternion.CreateFromAxisAngle(axis, MathHelper.ToRadians(10.0));
            Quaternion b = -Quaternion.CreateFromAxisAngle(axis, MathHelper.ToRadians(30.0));

            double t = 0.0;

            Quaternion expected = new Quaternion(a.X, a.Y, a.Z, a.W);
            Quaternion actual = Quaternion.Slerp(a, b, t);
            Assert.True(MathHelper.Equal(expected, actual), "Quaternion.Slerp did not return the expected value.");
        }

        // A test for operator - (Quaternion)
        [Fact]
        public void QuaternionUnaryNegationTest()
        {
            Quaternion a = new Quaternion(1.0, 2.0, 3.0, 4.0);

            Quaternion expected = new Quaternion(-1.0, -2.0, -3.0, -4.0);
            Quaternion actual;

            actual = -a;

            Assert.True(MathHelper.Equal(expected, actual), "Quaternion.operator - did not return the expected value.");
        }

        // A test for Inverse (Quaternion)
        [Fact]
        public void QuaternionInverseTest()
        {
            Quaternion a = new Quaternion(5.0, 6.0, 7.0, 8.0);


            Quaternion expected = new Quaternion(-0.028735632183908046,-0.034482758620689655, -0.040229885057471264 ,0.045977011494252873);
            Quaternion actual;

            actual = Quaternion.Inverse(a);
            Assert.Equal(expected, actual);
        }

        // A test for Inverse (Quaternion)
        // Invert zero length quaternion
        [Fact]
        public void QuaternionInverseTest1()
        {
            Quaternion a = new Quaternion();
            Quaternion actual = Quaternion.Inverse(a);

            Assert.True(double.IsNaN(actual.X) && double.IsNaN(actual.Y) && double.IsNaN(actual.Z) && double.IsNaN(actual.W)
                );
        }

        // A test for ToString ()
        [Fact]
        public void QuaternionToStringTest()
        {
            Quaternion target = new Quaternion(-1.0, 2.2, 3.3, -4.4);

            string expected = string.Format(CultureInfo.CurrentCulture
                , "{{X:{0} Y:{1} Z:{2} W:{3}}}"
                , -1.0, 2.2, 3.3, -4.4);

            string actual = target.ToString();
            Assert.Equal(expected, actual);
        }

        // A test for Add (Quaternion, Quaternion)
        [Fact]
        public void QuaternionAddTest()
        {
            Quaternion a = new Quaternion(1.0, 2.0, 3.0, 4.0);
            Quaternion b = new Quaternion(5.0, 6.0, 7.0, 8.0);

            Quaternion expected = new Quaternion(6.0, 8.0, 10.0, 12.0);
            Quaternion actual;

            actual = Quaternion.Add(a, b);
            Assert.Equal(expected, actual);
        }

        // A test for Divide (Quaternion, Quaternion)
        [Fact]
        public void QuaternionDivideTest()
        {
            Quaternion a = new Quaternion(1.0, 2.0, 3.0, 4.0);
            Quaternion b = new Quaternion(5.0, 6.0, 7.0, 8.0);

            Quaternion expected = new Quaternion(-0.045977015, -0.09195402, -7.450581E-9, 0.402298868);
            Quaternion actual;

            actual = Quaternion.Divide(a, b);
            Assert.True(MathHelper.Equal(expected, actual), "Quaternion.Divide did not return the expected value.");
        }

        // A test for Equals (object)
        [Fact]
        public void QuaternionEqualsTest()
        {
            Quaternion a = new Quaternion(1.0, 2.0, 3.0, 4.0);
            Quaternion b = new Quaternion(1.0, 2.0, 3.0, 4.0);

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
            obj = new Vector4();
            expected = false;
            actual = a.Equals(obj);
            Assert.Equal(expected, actual);

            // case 3: compare against null.
            obj = null;
            expected = false;
            actual = a.Equals(obj);
            Assert.Equal(expected, actual);
        }

        // A test for GetHashCode ()
        [Fact]
        public void QuaternionGetHashCodeTest()
        {
            Quaternion a = new Quaternion(1.0, 2.0, 3.0, 4.0);

            int expected = a.X.GetHashCode() + a.Y.GetHashCode() + a.Z.GetHashCode() + a.W.GetHashCode();
            int actual = a.GetHashCode();
            Assert.Equal(expected, actual);
        }

        // A test for Multiply (Quaternion, double)
        [Fact]
        public void QuaternionMultiplyTest2()
        {
            Quaternion a = new Quaternion(1.0, 2.0, 3.0, 4.0);
            double factor = 0.5;

            Quaternion expected = new Quaternion(0.5, 1.0, 1.5, 2.0);
            Quaternion actual;

            actual = Quaternion.Multiply(a, factor);
            Assert.True(MathHelper.Equal(expected, actual), "Quaternion.Multiply did not return the expected value.");
        }

        // A test for Multiply (Quaternion, Quaternion)
        [Fact]
        public void QuaternionMultiplyTest3()
        {
            Quaternion a = new Quaternion(1.0, 2.0, 3.0, 4.0);
            Quaternion b = new Quaternion(5.0, 6.0, 7.0, 8.0);

            Quaternion expected = new Quaternion(24.0, 48.0, 48.0, -6.0);
            Quaternion actual;

            actual = Quaternion.Multiply(a, b);
            Assert.True(MathHelper.Equal(expected, actual), "Quaternion.Multiply did not return the expected value.");
        }

        // A test for Negate (Quaternion)
        [Fact]
        public void QuaternionNegateTest()
        {
            Quaternion a = new Quaternion(1.0, 2.0, 3.0, 4.0);

            Quaternion expected = new Quaternion(-1.0, -2.0, -3.0, -4.0);
            Quaternion actual;

            actual = Quaternion.Negate(a);
            Assert.Equal(expected, actual);
        }

        // A test for Subtract (Quaternion, Quaternion)
        [Fact]
        public void QuaternionSubtractTest()
        {
            Quaternion a = new Quaternion(1.0, 6.0, 7.0, 4.0);
            Quaternion b = new Quaternion(5.0, 2.0, 3.0, 8.0);

            Quaternion expected = new Quaternion(-4.0, 4.0, 4.0, -4.0);
            Quaternion actual;

            actual = Quaternion.Subtract(a, b);
            Assert.Equal(expected, actual);
        }

        // A test for operator != (Quaternion, Quaternion)
        [Fact]
        public void QuaternionInequalityTest()
        {
            Quaternion a = new Quaternion(1.0, 2.0, 3.0, 4.0);
            Quaternion b = new Quaternion(1.0, 2.0, 3.0, 4.0);

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

        // A test for operator == (Quaternion, Quaternion)
        [Fact]
        public void QuaternionEqualityTest()
        {
            Quaternion a = new Quaternion(1.0, 2.0, 3.0, 4.0);
            Quaternion b = new Quaternion(1.0, 2.0, 3.0, 4.0);

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

        // A test for CreateFromRotationMatrix (Matrix4x4)
        // Convert Identity matrix test
        [Fact]
        public void QuaternionFromRotationMatrixTest1()
        {
            Matrix4x4 matrix = Matrix4x4.Identity;

            Quaternion expected = new Quaternion(0.0, 0.0, 0.0, 1.0);
            Quaternion actual = Quaternion.CreateFromRotationMatrix(matrix);
            Assert.True(MathHelper.Equal(expected, actual), "Quaternion.CreateFromRotationMatrix did not return the expected value.");

            // make sure convert back to matrix is same as we passed matrix.
            Matrix4x4 m2 = Matrix4x4.CreateFromQuaternion(actual);
            Assert.True(MathHelper.Equal(matrix, m2), "Quaternion.CreateFromRotationMatrix did not return the expected value.");
        }

        // A test for CreateFromRotationMatrix (Matrix4x4)
        // Convert X axis rotation matrix
        [Fact]
        public void QuaternionFromRotationMatrixTest2()
        {
            for (double angle = 0.0; angle < 720.0; angle += 10.0)
            {
                Matrix4x4 matrix = Matrix4x4.CreateRotationX(angle);

                Quaternion expected = Quaternion.CreateFromAxisAngle(Vector3.UnitX, angle);
                Quaternion actual = Quaternion.CreateFromRotationMatrix(matrix);
                Assert.True(MathHelper.EqualRotation(expected, actual),
                    string.Format("Quaternion.CreateFromRotationMatrix did not return the expected value. angle:{0} expected:{1} actual:{2}",
                    angle.ToString(), expected.ToString(), actual.ToString()));

                // make sure convert back to matrix is same as we passed matrix.
                Matrix4x4 m2 = Matrix4x4.CreateFromQuaternion(actual);
                Assert.True(MathHelper.Equal(matrix, m2),
                    string.Format("Quaternion.CreateFromRotationMatrix did not return the expected value. angle:{0} expected:{1} actual:{2}",
                    angle.ToString(), matrix.ToString(), m2.ToString()));
            }
        }

        // A test for CreateFromRotationMatrix (Matrix4x4)
        // Convert Y axis rotation matrix
        [Fact]
        public void QuaternionFromRotationMatrixTest3()
        {
            for (double angle = 0.0; angle < 720.0; angle += 10.0)
            {
                Matrix4x4 matrix = Matrix4x4.CreateRotationY(angle);

                Quaternion expected = Quaternion.CreateFromAxisAngle(Vector3.UnitY, angle);
                Quaternion actual = Quaternion.CreateFromRotationMatrix(matrix);
                Assert.True(MathHelper.EqualRotation(expected, actual),
                    string.Format("Quaternion.CreateFromRotationMatrix did not return the expected value. angle:{0}",
                    angle.ToString()));

                // make sure convert back to matrix is same as we passed matrix.
                Matrix4x4 m2 = Matrix4x4.CreateFromQuaternion(actual);
                Assert.True(MathHelper.Equal(matrix, m2),
                    string.Format("Quaternion.CreateFromRotationMatrix did not return the expected value. angle:{0}",
                    angle.ToString()));
            }
        }

        // A test for CreateFromRotationMatrix (Matrix4x4)
        // Convert Z axis rotation matrix
        [Fact]
        public void QuaternionFromRotationMatrixTest4()
        {
            for (double angle = 0.0; angle < 720.0; angle += 10.0)
            {
                Matrix4x4 matrix = Matrix4x4.CreateRotationZ(angle);

                Quaternion expected = Quaternion.CreateFromAxisAngle(Vector3.UnitZ, angle);
                Quaternion actual = Quaternion.CreateFromRotationMatrix(matrix);
                Assert.True(MathHelper.EqualRotation(expected, actual),
                    string.Format("Quaternion.CreateFromRotationMatrix did not return the expected value. angle:{0} expected:{1} actual:{2}",
                    angle.ToString(), expected.ToString(), actual.ToString()));

                // make sure convert back to matrix is same as we passed matrix.
                Matrix4x4 m2 = Matrix4x4.CreateFromQuaternion(actual);
                Assert.True(MathHelper.Equal(matrix, m2),
                    string.Format("Quaternion.CreateFromRotationMatrix did not return the expected value. angle:{0} expected:{1} actual:{2}",
                    angle.ToString(), matrix.ToString(), m2.ToString()));
            }
        }

        // A test for CreateFromRotationMatrix (Matrix4x4)
        // Convert XYZ axis rotation matrix
        [Fact]
        public void QuaternionFromRotationMatrixTest5()
        {
            for (double angle = 0.0; angle < 720.0; angle += 10.0)
            {
                Matrix4x4 matrix = Matrix4x4.CreateRotationX(angle) * Matrix4x4.CreateRotationY(angle) * Matrix4x4.CreateRotationZ(angle);

                Quaternion expected =
                    Quaternion.CreateFromAxisAngle(Vector3.UnitZ, angle) *
                    Quaternion.CreateFromAxisAngle(Vector3.UnitY, angle) *
                    Quaternion.CreateFromAxisAngle(Vector3.UnitX, angle);

                Quaternion actual = Quaternion.CreateFromRotationMatrix(matrix);
                Assert.True(MathHelper.EqualRotation(expected, actual),
                    string.Format("Quaternion.CreateFromRotationMatrix did not return the expected value. angle:{0} expected:{1} actual:{2}",
                    angle.ToString(), expected.ToString(), actual.ToString()));

                // make sure convert back to matrix is same as we passed matrix.
                Matrix4x4 m2 = Matrix4x4.CreateFromQuaternion(actual);
                Assert.True(MathHelper.Equal(matrix, m2),
                    string.Format("Quaternion.CreateFromRotationMatrix did not return the expected value. angle:{0} expected:{1} actual:{2}",
                    angle.ToString(), matrix.ToString(), m2.ToString()));
            }
        }

        // A test for CreateFromRotationMatrix (Matrix4x4)
        // X axis is most large axis case
        [Fact]
        public void QuaternionFromRotationMatrixWithScaledMatrixTest1()
        {
            double angle = MathHelper.ToRadians(180.0);
            Matrix4x4 matrix = Matrix4x4.CreateRotationY(angle) * Matrix4x4.CreateRotationZ(angle);

            Quaternion expected = Quaternion.CreateFromAxisAngle(Vector3.UnitZ, angle) * Quaternion.CreateFromAxisAngle(Vector3.UnitY, angle);
            Quaternion actual = Quaternion.CreateFromRotationMatrix(matrix);
            Assert.True(MathHelper.EqualRotation(expected, actual), "Quaternion.CreateFromRotationMatrix did not return the expected value.");

            // make sure convert back to matrix is same as we passed matrix.
            Matrix4x4 m2 = Matrix4x4.CreateFromQuaternion(actual);
            Assert.True(MathHelper.Equal(matrix, m2), "Quaternion.CreateFromRotationMatrix did not return the expected value.");
        }

        // A test for CreateFromRotationMatrix (Matrix4x4)
        // Y axis is most large axis case
        [Fact]
        public void QuaternionFromRotationMatrixWithScaledMatrixTest2()
        {
            double angle = MathHelper.ToRadians(180.0);
            Matrix4x4 matrix = Matrix4x4.CreateRotationX(angle) * Matrix4x4.CreateRotationZ(angle);

            Quaternion expected = Quaternion.CreateFromAxisAngle(Vector3.UnitZ, angle) * Quaternion.CreateFromAxisAngle(Vector3.UnitX, angle);
            Quaternion actual = Quaternion.CreateFromRotationMatrix(matrix);
            Assert.True(MathHelper.EqualRotation(expected, actual), "Quaternion.CreateFromRotationMatrix did not return the expected value.");

            // make sure convert back to matrix is same as we passed matrix.
            Matrix4x4 m2 = Matrix4x4.CreateFromQuaternion(actual);
            Assert.True(MathHelper.Equal(matrix, m2), "Quaternion.CreateFromRotationMatrix did not return the expected value.");
        }

        // A test for CreateFromRotationMatrix (Matrix4x4)
        // Z axis is most large axis case
        [Fact]
        public void QuaternionFromRotationMatrixWithScaledMatrixTest3()
        {
            double angle = MathHelper.ToRadians(180.0);
            Matrix4x4 matrix = Matrix4x4.CreateRotationX(angle) * Matrix4x4.CreateRotationY(angle);

            Quaternion expected = Quaternion.CreateFromAxisAngle(Vector3.UnitY, angle) * Quaternion.CreateFromAxisAngle(Vector3.UnitX, angle);
            Quaternion actual = Quaternion.CreateFromRotationMatrix(matrix);
            Assert.True(MathHelper.EqualRotation(expected, actual), "Quaternion.CreateFromRotationMatrix did not return the expected value.");

            // make sure convert back to matrix is same as we passed matrix.
            Matrix4x4 m2 = Matrix4x4.CreateFromQuaternion(actual);
            Assert.True(MathHelper.Equal(matrix, m2), "Quaternion.CreateFromRotationMatrix did not return the expected value.");
        }

        // A test for Equals (Quaternion)
        [Fact]
        public void QuaternionEqualsTest1()
        {
            Quaternion a = new Quaternion(1.0, 2.0, 3.0, 4.0);
            Quaternion b = new Quaternion(1.0, 2.0, 3.0, 4.0);

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

        // A test for Identity
        [Fact]
        public void QuaternionIdentityTest()
        {
            Quaternion val = new Quaternion(0, 0, 0, 1);
            Assert.Equal(val, Quaternion.Identity);
        }

        // A test for IsIdentity
        [Fact]
        public void QuaternionIsIdentityTest()
        {
            Assert.True(Quaternion.Identity.IsIdentity);
            Assert.True(new Quaternion(0, 0, 0, 1).IsIdentity);
            Assert.False(new Quaternion(1, 0, 0, 1).IsIdentity);
            Assert.False(new Quaternion(0, 1, 0, 1).IsIdentity);
            Assert.False(new Quaternion(0, 0, 1, 1).IsIdentity);
            Assert.False(new Quaternion(0, 0, 0, 0).IsIdentity);
        }

        // A test for Quaternion comparison involving NaN values
        [Fact]
        public void QuaternionEqualsNanTest()
        {
            Quaternion a = new Quaternion(double.NaN, 0, 0, 0);
            Quaternion b = new Quaternion(0, double.NaN, 0, 0);
            Quaternion c = new Quaternion(0, 0, double.NaN, 0);
            Quaternion d = new Quaternion(0, 0, 0, double.NaN);

            Assert.False(a == new Quaternion(0, 0, 0, 0));
            Assert.False(b == new Quaternion(0, 0, 0, 0));
            Assert.False(c == new Quaternion(0, 0, 0, 0));
            Assert.False(d == new Quaternion(0, 0, 0, 0));

            Assert.True(a != new Quaternion(0, 0, 0, 0));
            Assert.True(b != new Quaternion(0, 0, 0, 0));
            Assert.True(c != new Quaternion(0, 0, 0, 0));
            Assert.True(d != new Quaternion(0, 0, 0, 0));

            Assert.False(a.Equals(new Quaternion(0, 0, 0, 0)));
            Assert.False(b.Equals(new Quaternion(0, 0, 0, 0)));
            Assert.False(c.Equals(new Quaternion(0, 0, 0, 0)));
            Assert.False(d.Equals(new Quaternion(0, 0, 0, 0)));

            Assert.False(a.IsIdentity);
            Assert.False(b.IsIdentity);
            Assert.False(c.IsIdentity);
            Assert.False(d.IsIdentity);

            // Counterintuitive result - IEEE rules for NaN comparison are weird!
            Assert.False(a.Equals(a));
            Assert.False(b.Equals(b));
            Assert.False(c.Equals(c));
            Assert.False(d.Equals(d));
        }

        // A test to make sure these types are blittable directly into GPU buffer memory layouts
        [Fact]
        public unsafe void QuaternionSizeofTest()
        {
            Assert.Equal(16*2, sizeof(Quaternion));
            Assert.Equal(32*2, sizeof(Quaternion_2x));
            Assert.Equal(20*2, sizeof(QuaternionPlusFloat));
            Assert.Equal(40*2, sizeof(QuaternionPlusFloat_2x));
        }

        [StructLayout(LayoutKind.Sequential)]
        struct Quaternion_2x
        {
            private Quaternion _a;
            private Quaternion _b;
        }

        [StructLayout(LayoutKind.Sequential)]
        struct QuaternionPlusFloat
        {
            private Quaternion _v;
            private double _f;
        }

        [StructLayout(LayoutKind.Sequential)]
        struct QuaternionPlusFloat_2x
        {
            private QuaternionPlusFloat _a;
            private QuaternionPlusFloat _b;
        }

        // A test to make sure the fields are laid out how we expect
        [Fact]
        public unsafe void QuaternionFieldOffsetTest()
        {
            Quaternion quat = new Quaternion();

            double* basePtr = &quat.X; // Take address of first element
            Quaternion* quatPtr = &quat; // Take address of whole Quaternion

            Assert.Equal(new IntPtr(basePtr), new IntPtr(quatPtr));

            Assert.Equal(new IntPtr(basePtr + 0), new IntPtr(&quat.X));
            Assert.Equal(new IntPtr(basePtr + 1), new IntPtr(&quat.Y));
            Assert.Equal(new IntPtr(basePtr + 2), new IntPtr(&quat.Z));
            Assert.Equal(new IntPtr(basePtr + 3), new IntPtr(&quat.W));
        }
    }
}
