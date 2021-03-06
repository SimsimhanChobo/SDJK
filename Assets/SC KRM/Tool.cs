using ExtendedNumerics;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Numerics;
using System.Text;
using UnityEngine;
using Vector2 = UnityEngine.Vector2;
using Vector3 = UnityEngine.Vector3;
using Vector4 = UnityEngine.Vector4;

namespace SCKRM
{
    public static class MathTool
    {
        #region Trigonometric functions
        public static float Sin(this float value) => (float)Math.Sin(value);
        public static double Sin(this double value) => Math.Sin(value);
        public static decimal Sin(this decimal value) => (decimal)Math.Sin((double)value);

        public static float Asin(this float value) => (float)Math.Asin(value);
        public static double Asin(this double value) => Math.Asin(value);
        public static decimal Asin(this decimal value) => (decimal)Math.Asin((double)value);

        public static float Cos(this float value) => (float)Math.Cos(value);
        public static double Cos(this double value) => Math.Cos(value);
        public static decimal Cos(this decimal value) => (decimal)Math.Cos((double)value);

        public static float Acos(this float value) => (float)Math.Acos(value);
        public static double Acos(this double value) => Math.Acos(value);
        public static decimal Acos(this decimal value) => (decimal)Math.Acos((double)value);

        public static float Tan(this float value) => (float)Math.Tan(value);
        public static double Tan(this double value) => Math.Tan(value);
        public static decimal Tan(this decimal value) => (decimal)Math.Tan((double)value);

        public static float Atan(this float value) => (float)Math.Atan(value);
        public static double Atan(this double value) => Math.Atan(value);
        public static decimal Atan(this decimal value) => (decimal)Math.Atan((double)value);
        #endregion

        #region Abs
        public static sbyte Abs(this sbyte value)
        {
            if (value < 0)
                return (sbyte)-value;
            else
                return value;
        }

        public static short Abs(this short value)
        {
            if (value < 0)
                return (sbyte)-value;
            else
                return value;
        }

        public static int Abs(this int value)
        {
            if (value < 0)
                return -value;
            else
                return value;
        }

        public static long Abs(this long value)
        {
            if (value < 0)
                return -value;
            else
                return value;
        }

        public static float Abs(this float value)
        {
            if (value < 0)
                return -value;
            else
                return value;
        }

        public static double Abs(this double value)
        {
            if (value < 0)
                return -value;
            else
                return value;
        }

        public static decimal Abs(this decimal value)
        {
            if (value < 0)
                return -value;
            else
                return value;
        }

        public static BigInteger Abs(this BigInteger value)
        {
            if (value < 0)
                return -value;
            else
                return value;
        }

        public static BigDecimal Abs(this BigDecimal value)
        {
            if (value < 0)
                return -value;
            else
                return value;
        }

        public static nint Abs(this nint value)
        {
            if (value < 0)
                return -value;
            else
                return value;
        }
        #endregion Abs

        #region Sign
        public static int Sign(this sbyte value)
        {
            if (value < 0)
                return -1;
            else
                return 1;
        }

        public static short Sign(this short value)
        {
            if (value < 0)
                return -1;
            else
                return 1;
        }

        public static int Sign(this int value)
        {
            if (value < 0)
                return -1;
            else
                return 1;
        }

        public static long Sign(this long value)
        {
            if (value < 0)
                return -1;
            else
                return 1;
        }

        public static int Sign(this float value)
        {
            if (value < 0)
                return -1;
            else
                return 1;
        }

        public static int Sign(this double value)
        {
            if (value < 0)
                return -1;
            else
                return 1;
        }

        public static int Sign(this decimal value)
        {
            if (value < 0)
                return -1;
            else
                return 1;
        }

        public static int Sign(this BigInteger value)
        {
            if (value < BigInteger.Zero)
                return -1;
            else
                return 1;
        }

        public static int Sign(this BigDecimal value)
        {
            if (value < BigDecimal.Zero)
                return -1;
            else
                return 1;
        }

        public static int Sign(this nint value)
        {
            if (value < 0)
                return -1;
            else
                return 1;
        }

        public static int Sign(this nuint value)
        {
            if (value < 0)
                return -1;
            else
                return 1;
        }
        #endregion

        #region Clamp
        public static byte Clamp(this byte value, byte min, byte max = byte.MaxValue)
        {
            if (value < min)
                return min;
            else if (value > max)
                return max;
            else
                return value;
        }

        public static sbyte Clamp(this sbyte value, sbyte min, sbyte max = sbyte.MaxValue)
        {
            if (value < min)
                return min;
            else if (value > max)
                return max;
            else
                return value;
        }

        public static short Clamp(this short value, short min, short max = short.MaxValue)
        {
            if (value < min)
                return min;
            else if (value > max)
                return max;
            else
                return value;
        }

        public static ushort Clamp(this ushort value, ushort min, ushort max = ushort.MaxValue)
        {
            if (value < min)
                return min;
            else if (value > max)
                return max;
            else
                return value;
        }

        public static int Clamp(this int value, int min, int max = int.MaxValue)
        {
            if (value < min)
                return min;
            else if (value > max)
                return max;
            else
                return value;
        }

        public static uint Clamp(this uint value, uint min, uint max = uint.MaxValue)
        {
            if (value < min)
                return min;
            else if (value > max)
                return max;
            else
                return value;
        }

        public static long Clamp(this long value, long min, long max = long.MaxValue)
        {
            if (value < min)
                return min;
            else if (value > max)
                return max;
            else
                return value;
        }

        public static ulong Clamp(this ulong value, ulong min, ulong max = ulong.MaxValue)
        {
            if (value < min)
                return min;
            else if (value > max)
                return max;
            else
                return value;
        }

        public static float Clamp(this float value, float min, float max = float.PositiveInfinity)
        {
            if (float.IsNaN(value))
                return 0;

            if (value < min || float.IsNegativeInfinity(value))
                return min;
            else if (value > max || float.IsPositiveInfinity(value))
                return max;
            else
                return value;
        }

        public static double Clamp(this double value, double min, double max = double.PositiveInfinity)
        {
            if (double.IsNaN(value))
                return 0;

            if (value < min || double.IsNegativeInfinity(value))
                return min;
            else if (value > max || double.IsPositiveInfinity(value))
                return max;
            else
                return value;
        }

        public static decimal Clamp(this decimal value, decimal min, decimal max = decimal.MaxValue)
        {
            if (value < min)
                return min;
            else if (value > max)
                return max;
            else
                return value;
        }

        public static BigInteger Clamp(this BigInteger value, BigInteger min)
        {
            if (value < min)
                return min;
            else
                return value;
        }

        public static BigInteger Clamp(this BigInteger value, BigInteger min, BigInteger max)
        {
            if (value < min)
                return min;
            else if (value > max)
                return max;
            else
                return value;
        }

        public static BigDecimal Clamp(this BigDecimal value, BigDecimal min)
        {
            if (value < min)
                return min;
            else
                return value;
        }

        public static BigDecimal Clamp(this BigDecimal value, BigDecimal min, BigDecimal max)
        {
            if (value < min)
                return min;
            else if (value > max)
                return max;
            else
                return value;
        }

        public static nint Clamp(this nint value, nint min)
        {
            if (value < min)
                return min;
            else
                return value;
        }

        public static nint Clamp(this nint value, nint min, nint max)
        {
            if (value < min)
                return min;
            else if (value > max)
                return max;
            else
                return value;
        }

        public static nuint Clamp(this nuint value, nuint min)
        {
            if (value < min)
                return min;
            else
                return value;
        }

        public static nuint Clamp(this nuint value, nuint min, nuint max)
        {
            if (value < min)
                return min;
            else if (value > max)
                return max;
            else
                return value;
        }
        #endregion

        #region Clamp01
        public static byte Clamp01(this byte value)
        {
            if (value > 1)
                return 1;
            else
                return value;
        }

        public static sbyte Clamp01(this sbyte value)
        {
            if (value < 0)
                return 0;
            else if (value > 1)
                return 1;
            else
                return value;
        }

        public static short Clamp01(this short value)
        {
            if (value < 0)
                return 0;
            else if (value > 1)
                return 1;
            else
                return value;
        }

        public static ushort Clamp01(this ushort value)
        {
            if (value > 1)
                return 1;
            else
                return value;
        }

        public static int Clamp01(this int value)
        {
            if (value < 0)
                return 0;
            else if (value > 1)
                return 1;
            else
                return value;
        }

        public static uint Clamp01(this uint value)
        {
            if (value > 1)
                return 1;
            else
                return value;
        }

        public static long Clamp01(this long value)
        {
            if (value < 0)
                return 0;
            else if (value > 1)
                return 1;
            else
                return value;
        }

        public static ulong Clamp01(this ulong value)
        {
            if (value > 1)
                return 1;
            else
                return value;
        }

        public static float Clamp01(this float value)
        {
            if (value < 0)
                return 0;
            else if (value > 1)
                return 1;
            else
                return value;
        }

        public static double Clamp01(this double value)
        {
            if (value < 0)
                return 0;
            else if (value > 1)
                return 1;
            else
                return value;
        }

        public static decimal Clamp01(this decimal value)
        {
            if (value < 0)
                return 0;
            else if (value > 1)
                return 1;
            else
                return value;
        }

        public static BigInteger Clamp01(this BigInteger value)
        {
            if (value < 0)
                return BigInteger.Zero;
            else if (value > 1)
                return BigInteger.One;
            else
                return value;
        }

        public static BigDecimal Clamp01(this BigDecimal value)
        {
            if (value < 0)
                return BigDecimal.Zero;
            else if (value > 1)
                return BigDecimal.One;
            else
                return value;
        }

        public static nint Clamp01(this nint value)
        {
            if (value < 0)
                return 0;
            else if (value > 1)
                return 1;
            else
                return value;
        }

        public static nuint Clamp01(this nuint value)
        {
            if (value < 0)
                return 0;
            else if (value > 1)
                return 1;
            else
                return value;
        }
        #endregion

        #region Reapeat
        public static sbyte Reapeat(this sbyte t, sbyte length) => (sbyte)(t - (t / length) * length).Clamp(0, length);
        public static byte Reapeat(this byte t, byte length) => (byte)(t - (t / length) * length).Clamp(0, length);
        public static short Reapeat(this short t, short length) => (short)(t - (t / length) * length).Clamp(0, length);
        public static ushort Reapeat(this ushort t, ushort length) => (ushort)(t - (t / length) * length).Clamp(0, length);
        public static int Reapeat(this int t, int length) => (t - (t / length) * length).Clamp(0, length);
        public static uint Reapeat(this uint t, uint length) => (t - (t / length) * length).Clamp(0, length);
        public static long Reapeat(this long t, long length) => (t - (t / length) * length).Clamp(0, length);
        public static ulong Reapeat(this ulong t, ulong length) => (t - (t / length) * length).Clamp(0, length);
        public static float Reapeat(this float t, float length) => (t - (t / length).Floor() * length).Clamp(0, length);
        public static double Reapeat(this double t, double length) => (t - (t / length).Floor() * length).Clamp(0, length);
        public static decimal Reapeat(this decimal t, decimal length) => (t - (t / length).Floor() * length).Clamp(0, length);
        public static BigInteger Reapeat(this BigInteger t, BigInteger length) => (t - (t / length) * length).Clamp(0, length);
        public static BigDecimal Reapeat(this BigDecimal t, BigDecimal length) => (t - (t / length).Floor() * length).Clamp(0, length);
        public static nint Reapeat(this nint t, nint length) => (t - (t / length) * length).Clamp(0, length);
        public static nuint Reapeat(this nuint t, nuint length) => (t - (t / length) * length).Clamp(0, length);
        #endregion

        #region Lerp
        public static byte Lerp(this byte current, byte target, byte t, bool unclamped = false)
        {
            if (!unclamped)
                t = t.Clamp01();
            return (byte)(((1 - t) * current) + (target * t));
        }

        public static byte Lerp(this byte current, byte target, float t, bool unclamped = false)
        {
            if (!unclamped)
                t = t.Clamp01();
            return (byte)(((1 - t) * current) + (target * t));
        }

        public static byte Lerp(this byte current, byte target, double t, bool unclamped = false)
        {
            if (!unclamped)
                t = t.Clamp01();
            return (byte)(((1 - t) * current) + (target * t));
        }

        public static byte Lerp(this byte current, byte target, decimal t, bool unclamped = false)
        {
            if (!unclamped)
                t = t.Clamp01();
            return (byte)(((1 - t) * current) + (target * t));
        }

        public static sbyte Lerp(this sbyte current, sbyte target, sbyte t, bool unclamped = false)
        {
            if (!unclamped)
                t = t.Clamp01();
            return (sbyte)(((1 - t) * current) + (target * t));
        }

        public static sbyte Lerp(this sbyte current, sbyte target, float t, bool unclamped = false)
        {
            if (!unclamped)
                t = t.Clamp01();
            return (sbyte)(((1 - t) * current) + (target * t));
        }

        public static sbyte Lerp(this sbyte current, sbyte target, double t, bool unclamped = false)
        {
            if (!unclamped)
                t = t.Clamp01();
            return (sbyte)(((1 - t) * current) + (target * t));
        }

        public static sbyte Lerp(this sbyte current, sbyte target, decimal t, bool unclamped = false)
        {
            if (!unclamped)
                t = t.Clamp01();
            return (sbyte)(((1 - t) * current) + (target * t));
        }

        public static short Lerp(this short current, short target, short t, bool unclamped = false)
        {
            if (!unclamped)
                t = t.Clamp01();
            return (short)(((1 - t) * current) + (target * t));
        }

        public static short Lerp(this short current, short target, float t, bool unclamped = false)
        {
            if (!unclamped)
                t = t.Clamp01();
            return (short)(((1 - t) * current) + (target * t));
        }

        public static short Lerp(this short current, short target, double t, bool unclamped = false)
        {
            if (!unclamped)
                t = t.Clamp01();
            return (short)(((1 - t) * current) + (target * t));
        }

        public static short Lerp(this short current, short target, decimal t, bool unclamped = false)
        {
            if (!unclamped)
                t = t.Clamp01();
            return (short)(((1 - t) * current) + (target * t));
        }

        public static ushort Lerp(this ushort current, ushort target, ushort t, bool unclamped = false)
        {
            if (!unclamped)
                t = t.Clamp01();
            return (ushort)(((1 - t) * current) + (target * t));
        }

        public static ushort Lerp(this ushort current, ushort target, float t, bool unclamped = false)
        {
            if (!unclamped)
                t = t.Clamp01();
            return (ushort)(((1 - t) * current) + (target * t));
        }

        public static ushort Lerp(this ushort current, ushort target, double t, bool unclamped = false)
        {
            if (!unclamped)
                t = t.Clamp01();
            return (ushort)(((1 - t) * current) + (target * t));
        }

        public static ushort Lerp(this ushort current, ushort target, decimal t, bool unclamped = false)
        {
            if (!unclamped)
                t = t.Clamp01();
            return (ushort)(((1 - t) * current) + (target * t));
        }

        public static int Lerp(this int current, int target, int t, bool unclamped = false)
        {
            if (!unclamped)
                t = t.Clamp01();
            return (int)(((1 - t) * current) + (target * t));
        }

        public static int Lerp(this int current, int target, float t, bool unclamped = false)
        {
            if (!unclamped)
                t = t.Clamp01();
            return (int)(((1 - t) * current) + (target * t));
        }

        public static int Lerp(this int current, int target, double t, bool unclamped = false)
        {
            if (!unclamped)
                t = t.Clamp01();
            return (int)(((1 - t) * current) + (target * t));
        }

        public static int Lerp(this int current, int target, decimal t, bool unclamped = false)
        {
            if (!unclamped)
                t = t.Clamp01();
            return (int)(((1 - t) * current) + (target * t));
        }

        public static uint Lerp(this uint current, uint target, uint t, bool unclamped = false)
        {
            if (!unclamped)
                t = t.Clamp01();
            return (uint)(((1 - t) * current) + (target * t));
        }

        public static uint Lerp(this uint current, uint target, float t, bool unclamped = false)
        {
            if (!unclamped)
                t = t.Clamp01();
            return (uint)(((1 - t) * current) + (target * t));
        }

        public static uint Lerp(this uint current, uint target, double t, bool unclamped = false)
        {
            if (!unclamped)
                t = t.Clamp01();
            return (uint)(((1 - t) * current) + (target * t));
        }

        public static uint Lerp(this uint current, uint target, decimal t, bool unclamped = false)
        {
            if (!unclamped)
                t = t.Clamp01();
            return (uint)(((1 - t) * current) + (target * t));
        }

        public static long Lerp(this long current, long target, long t, bool unclamped = false)
        {
            if (!unclamped)
                t = t.Clamp01();
            return (long)(((1 - t) * current) + (target * t));
        }

        public static long Lerp(this long current, long target, float t, bool unclamped = false)
        {
            if (!unclamped)
                t = t.Clamp01();
            return (long)(((1 - t) * current) + (target * t));
        }

        public static long Lerp(this long current, long target, double t, bool unclamped = false)
        {
            if (!unclamped)
                t = t.Clamp01();
            return (long)(((1 - t) * current) + (target * t));
        }

        public static long Lerp(this long current, long target, decimal t, bool unclamped = false)
        {
            if (!unclamped)
                t = t.Clamp01();
            return (long)(((1 - t) * current) + (target * t));
        }

        public static ulong Lerp(this ulong current, ulong target, ulong t, bool unclamped = false)
        {
            if (!unclamped)
                t = t.Clamp01();
            return (ulong)(((1 - t) * current) + (target * t));
        }

        public static ulong Lerp(this ulong current, ulong target, float t, bool unclamped = false)
        {
            if (!unclamped)
                t = t.Clamp01();
            return (ulong)(((1 - t) * current) + (target * t));
        }

        public static ulong Lerp(this ulong current, ulong target, double t, bool unclamped = false)
        {
            if (!unclamped)
                t = t.Clamp01();
            return (ulong)(((1 - t) * current) + (target * t));
        }

        public static ulong Lerp(this ulong current, ulong target, decimal t, bool unclamped = false)
        {
            if (!unclamped)
                t = t.Clamp01();
            return (ulong)(((1 - t) * current) + (target * t));
        }

        public static float Lerp(this float current, float target, float t, bool unclamped = false)
        {
            if (!unclamped)
                t = t.Clamp01();
            return ((1 - t) * current) + (target * t);
        }

        public static double Lerp(this double current, double target, double t, bool unclamped = false)
        {
            if (!unclamped)
                t = t.Clamp01();
            return ((1 - t) * current) + (target * t);
        }

        public static decimal Lerp(this decimal current, decimal target, decimal t, bool unclamped = false)
        {
            if (!unclamped)
                t = t.Clamp01();
            return ((1 - t) * current) + (target * t);
        }

        public static BigInteger Lerp(this BigInteger current, BigInteger target, BigInteger t, bool unclamped = false)
        {
            if (!unclamped)
                t = t.Clamp01();
            return ((1 - t) * current) + (target * t);
        }

        public static BigDecimal Lerp(this BigDecimal current, BigDecimal target, BigDecimal t, bool unclamped = false)
        {
            if (!unclamped)
                t = t.Clamp01();
            return ((1 - t) * current) + (target * t);
        }

        public static nint Lerp(this nint current, nint target, float t, bool unclamped = false)
        {
            if (!unclamped)
                t = t.Clamp01();
            return (nint)(((1 - t) * current) + (target * t));
        }

        public static nint Lerp(this nint current, nint target, double t, bool unclamped = false)
        {
            if (!unclamped)
                t = t.Clamp01();
            return (nint)(((1 - t) * current) + (target * t));
        }

        public static nint Lerp(this nint current, nint target, decimal t, bool unclamped = false)
        {
            if (!unclamped)
                t = t.Clamp01();
            return (nint)(((1 - t) * current) + (target * t));
        }

        public static nuint Lerp(this nuint current, nuint target, float t, bool unclamped = false)
        {
            if (!unclamped)
                t = t.Clamp01();
            return (nuint)(((1 - t) * current) + (target * t));
        }

        public static nuint Lerp(this nuint current, nuint target, double t, bool unclamped = false)
        {
            if (!unclamped)
                t = t.Clamp01();
            return (nuint)(((1 - t) * current) + (target * t));
        }

        public static nuint Lerp(this nuint current, nuint target, decimal t, bool unclamped = false)
        {
            if (!unclamped)
                t = t.Clamp01();
            return (nuint)(((1 - t) * current) + (target * t));
        }

        public static Vector2 Lerp(this Vector2 current, Vector2 target, float t, bool unclamped = false)
        {
            if (!unclamped)
                t = t.Clamp01();
            return new Vector2(current.x + (target.x - current.x) * t, current.y + (target.y - current.y) * t);
        }

        public static Vector3 Lerp(this Vector3 current, Vector3 target, float t, bool unclamped = false)
        {
            if (!unclamped)
                t = t.Clamp01();
            return new Vector3(current.x + (target.x - current.x) * t, current.y + (target.y - current.y) * t, current.z + (target.z - current.z) * t);
        }

        public static Vector4 Lerp(this Vector4 current, Vector4 target, float t, bool unclamped = false)
        {
            if (!unclamped)
                t = t.Clamp01();
            return new Vector4(current.x + (target.x - current.x) * t, current.y + (target.y - current.y) * t, current.z + (target.z - current.z) * t, current.w + (target.w - current.w) * t);
        }

        public static Rect Lerp(this Rect current, Rect target, float t, bool unclamped = false)
        {
            if (!unclamped)
                t = t.Clamp01();
            return new Rect(current.x + (target.x - current.x) * t, current.y + (target.y - current.y) * t, current.width + (target.width - current.width) * t, current.height + (target.height - current.height) * t);
        }

        public static Color Lerp(this Color current, Color target, float t, bool alpha = true, bool unclamped = false)
        {
            if (!unclamped)
                t = t.Clamp01();

            if (alpha)
                return new Color(current.r + (target.r - current.r) * t, current.g + (target.g - current.g) * t, current.b + (target.b - current.b) * t, current.a + (target.a - current.a) * t);
            else
                return new Color(current.r + (target.r - current.r) * t, current.g + (target.g - current.g) * t, current.b + (target.b - current.b) * t, current.a);
        }
        #endregion

        #region MoveTowards
        public static byte MoveTowards(this byte current, byte target, byte maxDelta)
        {
            if ((target - current) <= maxDelta)
                return target;

            return (byte)(current + maxDelta);
        }

        public static sbyte MoveTowards(this sbyte current, sbyte target, sbyte maxDelta)
        {
            if ((target - current).Abs() <= maxDelta)
                return target;

            return (sbyte)(current + (target - current).Sign() * maxDelta);
        }

        public static short MoveTowards(this short current, short target, short maxDelta)
        {
            if ((target - current).Abs() <= maxDelta)
                return target;

            return (short)(current + (target - current).Sign() * maxDelta);
        }

        public static ushort MoveTowards(this ushort current, ushort target, ushort maxDelta)
        {
            if ((target - current) <= maxDelta)
                return target;

            return (ushort)(current + maxDelta);
        }

        public static int MoveTowards(this int current, int target, int maxDelta)
        {
            if ((target - current).Abs() <= maxDelta)
                return target;

            return current + (target - current).Sign() * maxDelta;
        }

        public static uint MoveTowards(this uint current, uint target, uint maxDelta)
        {
            if ((target - current) <= maxDelta)
                return target;

            return current + maxDelta;
        }

        public static long MoveTowards(this long current, long target, long maxDelta)
        {
            if ((target - current).Abs() <= maxDelta)
                return target;

            return current + (target - current).Sign() * maxDelta;
        }

        public static ulong MoveTowards(this ulong current, ulong target, ulong maxDelta)
        {
            if ((target - current) <= maxDelta)
                return target;

            return current + maxDelta;
        }

        public static float MoveTowards(this float current, float target, float maxDelta)
        {
            if ((target - current).Abs() <= maxDelta)
                return target;

            return current + (target - current).Sign() * maxDelta;
        }

        public static double MoveTowards(this double current, double target, double maxDelta)
        {
            if ((target - current).Abs() <= maxDelta)
                return target;

            return current + (target - current).Sign() * maxDelta;
        }

        public static decimal MoveTowards(this decimal current, decimal target, decimal maxDelta)
        {
            if ((target - current).Abs() <= maxDelta)
                return target;

            return current + (target - current).Sign() * maxDelta;
        }

        public static BigInteger MoveTowards(this BigInteger current, BigInteger target, BigInteger maxDelta)
        {
            if ((target - current).Abs() <= maxDelta)
                return target;

            return current + (target - current).Sign() * maxDelta;
        }

        public static BigDecimal MoveTowards(this BigDecimal current, BigDecimal target, BigDecimal maxDelta)
        {
            if ((target - current).Abs() <= maxDelta)
                return target;

            return current + (target - current).Sign() * maxDelta;
        }

        public static nint MoveTowards(this nint current, nint target, nint maxDelta)
        {
            if ((target - current).Abs() <= maxDelta)
                return target;

            return current + (target - current).Sign() * maxDelta;
        }

        public static nuint MoveTowards(this nuint current, nuint target, nuint maxDelta)
        {
            if ((target - current) <= maxDelta)
                return target;

            return current + maxDelta;
        }

        public static Vector2 MoveTowards(this Vector2 current, Vector2 target, float maxDistanceDelta)
        {
            float num = target.x - current.x;
            float num2 = target.y - current.y;
            float num3 = num * num + num2 * num2;
            if (num3 == 0f || (maxDistanceDelta >= 0f && num3 <= maxDistanceDelta * maxDistanceDelta))
                return target;

            float num4 = (float)Math.Sqrt(num3);
            return new Vector2(current.x + num / num4 * maxDistanceDelta, current.y + num2 / num4 * maxDistanceDelta);
        }
        public static Vector3 MoveTowards(this Vector3 current, Vector3 target, float maxDistanceDelta)
        {
            float num = target.x - current.x;
            float num2 = target.y - current.y;
            float num3 = target.z - current.z;
            float num4 = num * num + num2 * num2 + num3 * num3;
            if (num4 == 0f || (maxDistanceDelta >= 0f && num4 <= maxDistanceDelta * maxDistanceDelta))
                return target;

            float num5 = (float)Math.Sqrt(num4);
            return new Vector3(current.x + num / num5 * maxDistanceDelta, current.y + num2 / num5 * maxDistanceDelta, current.z + num3 / num5 * maxDistanceDelta);
        }
        public static Vector4 MoveTowards(this Vector4 current, Vector4 target, float maxDistanceDelta)
        {
            float num = target.x - current.x;
            float num2 = target.y - current.y;
            float num3 = target.z - current.z;
            float num4 = target.w - current.w;
            float num5 = num * num + num2 * num2 + num3 * num3 + num4 * num4;
            if (num5 == 0f || (maxDistanceDelta >= 0f && num5 <= maxDistanceDelta * maxDistanceDelta))
                return target;

            float num6 = (float)Math.Sqrt(num5);
            return new Vector4(current.x + num / num6 * maxDistanceDelta, current.y + num2 / num6 * maxDistanceDelta, current.z + num3 / num6 * maxDistanceDelta, current.w + num4 / num6 * maxDistanceDelta);
        }
        public static Rect MoveTowards(this Rect current, Rect target, float maxDistanceDelta)
        {
            float num = target.x - current.x;
            float num2 = target.y - current.y;
            float num3 = target.width - current.width;
            float num4 = target.height - current.height;
            float num5 = num * num + num2 * num2 + num3 * num3 + num4 * num4;
            if (num5 == 0f || (maxDistanceDelta >= 0f && num5 <= maxDistanceDelta * maxDistanceDelta))
                return target;

            float num6 = (float)Math.Sqrt(num5);
            return new Rect(current.x + num / num6 * maxDistanceDelta, current.y + num2 / num6 * maxDistanceDelta, current.width + num3 / num6 * maxDistanceDelta, current.height + num4 / num6 * maxDistanceDelta);
        }
        public static Color MoveTowards(this Color current, Color target, float maxDistanceDelta)
        {
            float num = target.r - current.r;
            float num2 = target.g - current.g;
            float num3 = target.b - current.b;
            float num4 = target.a - current.a;
            float num5 = num * num + num2 * num2 + num3 * num3 + num4 * num4;
            if (num5 == 0f || (maxDistanceDelta >= 0f && num5 <= maxDistanceDelta * maxDistanceDelta))
                return target;

            float num6 = (float)Math.Sqrt(num5);
            return new Color(current.r + num / num6 * maxDistanceDelta, current.g + num2 / num6 * maxDistanceDelta, current.b + num3 / num6 * maxDistanceDelta, current.a + num4 / num6 * maxDistanceDelta);
        }
        #endregion

        #region Ceil
        public static float Ceil(this float value) => (float)Math.Ceiling(value);
        public static double Ceil(this double value) => Math.Ceiling(value);
        public static decimal Ceil(this decimal value) => Math.Ceiling(value);
        public static BigDecimal Ceil(this BigDecimal value) => BigDecimal.Ceiling(value);

        public static int CeilToInt(this float value) => (int)Math.Ceiling(value);
        public static int CeilToInt(this double value) => (int)Math.Ceiling(value);
        public static int CeilToInt(this decimal value) => (int)Math.Ceiling(value);
        public static BigInteger CeilToInt(this BigDecimal value) => (BigInteger)BigDecimal.Ceiling(value);
        #endregion

        #region Floor
        public static float Floor(this float value) => (float)Math.Floor(value);
        public static double Floor(this double value) => Math.Floor(value);
        public static decimal Floor(this decimal value) => Math.Floor(value);
        public static BigDecimal Floor(this BigDecimal value) => BigDecimal.Floor(value);

        public static int FloorToInt(this float value) => (int)Math.Floor(value);
        public static int FloorToInt(this double value) => (int)Math.Floor(value);
        public static int FloorToInt(this decimal value) => (int)Math.Floor(value);
        public static BigInteger FloorToInt(this BigDecimal value) => (BigInteger)BigDecimal.Floor(value);
        #endregion

        #region Round
        public static float Round(this float value) => (float)Math.Round(value);
        public static double Round(this double value) => Math.Round(value);
        public static decimal Round(this decimal value) => Math.Round(value);
        public static BigDecimal Round(this BigDecimal value) => BigDecimal.Round(value);

        public static int RoundToInt(this float value) => (int)Math.Round(value);
        public static int RoundToInt(this double value) => (int)Math.Round(value);
        public static int RoundToInt(this decimal value) => (int)Math.Round(value);
        public static BigInteger RoundToInt(this BigDecimal value) => BigDecimal.Round(value);

        public static float Round(this float value, int digits) => (float)Math.Round(value, digits);
        public static double Round(this double value, int digits) => Math.Round(value, digits);
        public static decimal Round(this decimal value, int digits) => Math.Round(value, digits);
        #endregion

        #region Pow
        public static float Pow(this float x, float y) => (float)Math.Pow(x, y);
        public static double Pow(this double x, double y) => Math.Pow(x, y);
        public static decimal Pow(this decimal x, decimal y) => (decimal)Math.Pow((double)x, (double)y);
        #endregion

        #region Sqrt
        public static float Sqrt(this float value) => (float)Math.Sqrt(value);
        public static double Sqrt(this double value) => Math.Sqrt(value);
        public static decimal Sqrt(this decimal value) => (decimal)Math.Sqrt((double)value);
        #endregion

        #region Min
        public static sbyte Min(this sbyte a, sbyte b)
        {
            if (a < b)
                return a;
            else
                return b;
        }

        public static byte Min(this byte a, byte b)
        {
            if (a < b)
                return a;
            else
                return b;
        }

        public static short Min(this short a, short b)
        {
            if (a < b)
                return a;
            else
                return b;
        }

        public static ushort Min(this ushort a, ushort b)
        {
            if (a < b)
                return a;
            else
                return b;
        }

        public static int Min(this int a, int b)
        {
            if (a < b)
                return a;
            else
                return b;
        }

        public static uint Min(this uint a, uint b)
        {
            if (a < b)
                return a;
            else
                return b;
        }

        public static long Min(this long a, long b)
        {
            if (a < b)
                return a;
            else
                return b;
        }

        public static ulong Min(this ulong a, ulong b)
        {
            if (a < b)
                return a;
            else
                return b;
        }

        public static float Min(this float a, float b)
        {
            if (a < b)
                return a;
            else
                return b;
        }

        public static double Min(this double a, double b)
        {
            if (a < b)
                return a;
            else
                return b;
        }

        public static decimal Min(this decimal a, decimal b)
        {
            if (a < b)
                return a;
            else
                return b;
        }

        public static BigInteger Min(this BigInteger a, BigInteger b)
        {
            if (a < b)
                return a;
            else
                return b;
        }

        public static BigDecimal Min(this BigDecimal a, BigDecimal b)
        {
            if (a < b)
                return a;
            else
                return b;
        }

        public static nint Min(this nint a, nint b)
        {
            if (a < b)
                return a;
            else
                return b;
        }

        public static nuint Min(this nuint a, nuint b)
        {
            if (a < b)
                return a;
            else
                return b;
        }
        #endregion

        #region Min Array
        public static sbyte Min(this sbyte value, params sbyte[] values)
        {
            if (values == null)
                throw new ArgumentNullException();

            int length = values.Length;
            if (length == 0)
                return 0;

            sbyte num2 = value;
            for (int i = 0; i < length; i++)
            {
                if (values[i] < num2)
                    num2 = values[i];
            }

            return num2;
        }

        public static byte Min(this byte value, params byte[] values)
        {
            if (values == null)
                throw new ArgumentNullException();

            int length = values.Length;
            if (length == 0)
                return 0;

            byte num2 = value;
            for (int i = 0; i < length; i++)
            {
                if (values[i] < num2)
                    num2 = values[i];
            }

            return num2;
        }

        public static short Min(this short value, params short[] values)
        {
            if (values == null)
                throw new ArgumentNullException();

            int length = values.Length;
            if (length == 0)
                return 0;

            short num2 = value;
            for (int i = 0; i < length; i++)
            {
                if (values[i] < num2)
                    num2 = values[i];
            }

            return num2;
        }

        public static ushort Min(this ushort value, params ushort[] values)
        {
            if (values == null)
                throw new ArgumentNullException();

            int length = values.Length;
            if (length == 0)
                return 0;

            ushort num2 = value;
            for (int i = 0; i < length; i++)
            {
                if (values[i] < num2)
                    num2 = values[i];
            }

            return num2;
        }

        public static int Min(this int value, params int[] values)
        {
            if (values == null)
                throw new ArgumentNullException();

            int length = values.Length;
            if (length == 0)
                return 0;

            int num2 = value;
            for (int i = 0; i < length; i++)
            {
                if (values[i] < num2)
                    num2 = values[i];
            }

            return num2;
        }

        public static uint Min(this uint value, params uint[] values)
        {
            if (values == null)
                throw new ArgumentNullException();

            int length = values.Length;
            if (length == 0)
                return 0;

            uint num2 = value;
            for (int i = 0; i < length; i++)
            {
                if (values[i] < num2)
                    num2 = values[i];
            }

            return num2;
        }

        public static long Min(this long value, params long[] values)
        {
            if (values == null)
                throw new ArgumentNullException();

            int length = values.Length;
            if (length == 0)
                return 0;

            long num2 = value;
            for (int i = 0; i < length; i++)
            {
                if (values[i] < num2)
                    num2 = values[i];
            }

            return num2;
        }

        public static ulong Min(this ulong value, params ulong[] values)
        {
            if (values == null)
                throw new ArgumentNullException();

            int length = values.Length;
            if (length == 0)
                return 0;

            ulong num2 = value;
            for (int i = 0; i < length; i++)
            {
                if (values[i] < num2)
                    num2 = values[i];
            }

            return num2;
        }

        public static float Min(this float value, params float[] values)
        {
            if (values == null)
                throw new ArgumentNullException();

            int length = values.Length;
            if (length == 0)
                return 0;

            float num2 = value;
            for (int i = 0; i < length; i++)
            {
                if (values[i] < num2)
                    num2 = values[i];
            }

            return num2;
        }

        public static double Min(this double value, params double[] values)
        {
            if (values == null)
                throw new ArgumentNullException();

            int length = values.Length;
            if (length == 0)
                return 0;

            double num2 = value;
            for (int i = 0; i < length; i++)
            {
                if (values[i] < num2)
                    num2 = values[i];
            }

            return num2;
        }

        public static decimal Min(this decimal value, params decimal[] values)
        {
            if (values == null)
                throw new ArgumentNullException();

            int length = values.Length;
            if (length == 0)
                return 0;

            decimal num2 = value;
            for (int i = 0; i < length; i++)
            {
                if (values[i] < num2)
                    num2 = values[i];
            }

            return num2;
        }

        public static BigInteger Min(this BigInteger value, params BigInteger[] values)
        {
            if (values == null)
                throw new ArgumentNullException();

            int length = values.Length;
            if (length == 0)
                return 0;

            BigInteger num2 = value;
            for (int i = 0; i < length; i++)
            {
                if (values[i] < num2)
                    num2 = values[i];
            }

            return num2;
        }

        public static BigDecimal Min(this BigDecimal value, params BigDecimal[] values)
        {
            if (values == null)
                throw new ArgumentNullException();

            int length = values.Length;
            if (length == 0)
                return 0;

            BigDecimal num2 = value;
            for (int i = 0; i < length; i++)
            {
                if (values[i] < num2)
                    num2 = values[i];
            }

            return num2;
        }

        public static nint Min(this nint value, params nint[] values)
        {
            if (values == null)
                throw new ArgumentNullException();

            int length = values.Length;
            if (length == 0)
                return 0;

            nint num2 = value;
            for (int i = 0; i < length; i++)
            {
                if (values[i] < num2)
                    num2 = values[i];
            }

            return num2;
        }

        public static nuint Min(this nuint value, params nuint[] values)
        {
            if (values == null)
                throw new ArgumentNullException();

            int length = values.Length;
            if (length == 0)
                return 0;

            nuint num2 = value;
            for (int i = 0; i < length; i++)
            {
                if (values[i] < num2)
                    num2 = values[i];
            }

            return num2;
        }
        #endregion

        #region Max
        public static sbyte Max(this sbyte a, sbyte b)
        {
            if (a > b)
                return a;
            else
                return b;
        }

        public static byte Max(this byte a, byte b)
        {
            if (a > b)
                return a;
            else
                return b;
        }

        public static short Max(this short a, short b)
        {
            if (a > b)
                return a;
            else
                return b;
        }

        public static ushort Max(this ushort a, ushort b)
        {
            if (a > b)
                return a;
            else
                return b;
        }

        public static int Max(this int a, int b)
        {
            if (a > b)
                return a;
            else
                return b;
        }

        public static uint Max(this uint a, uint b)
        {
            if (a > b)
                return a;
            else
                return b;
        }

        public static long Max(this long a, long b)
        {
            if (a > b)
                return a;
            else
                return b;
        }

        public static ulong Max(this ulong a, ulong b)
        {
            if (a > b)
                return a;
            else
                return b;
        }

        public static float Max(this float a, float b)
        {
            if (a > b)
                return a;
            else
                return b;
        }

        public static double Max(this double a, double b)
        {
            if (a > b)
                return a;
            else
                return b;
        }

        public static decimal Max(this decimal a, decimal b)
        {
            if (a > b)
                return a;
            else
                return b;
        }

        public static BigInteger Max(this BigInteger a, BigInteger b)
        {
            if (a > b)
                return a;
            else
                return b;
        }

        public static BigDecimal Max(this BigDecimal a, BigDecimal b)
        {
            if (a > b)
                return a;
            else
                return b;
        }

        public static nint Max(this nint a, nint b)
        {
            if (a > b)
                return a;
            else
                return b;
        }

        public static nuint Max(this nuint a, nuint b)
        {
            if (a > b)
                return a;
            else
                return b;
        }
        #endregion

        #region Max Array
        public static sbyte Max(this sbyte value, params sbyte[] values)
        {
            if (values == null)
                throw new ArgumentNullException();

            int length = values.Length;
            if (length == 0)
                return 0;

            sbyte num2 = value;
            for (int i = 0; i < length; i++)
            {
                if (values[i] > num2)
                    num2 = values[i];
            }

            return num2;
        }

        public static byte Max(this byte value, params byte[] values)
        {
            if (values == null)
                throw new ArgumentNullException();

            int length = values.Length;
            if (length == 0)
                return 0;

            byte num2 = value;
            for (int i = 0; i < length; i++)
            {
                if (values[i] > num2)
                    num2 = values[i];
            }

            return num2;
        }

        public static short Max(this short value, params short[] values)
        {
            if (values == null)
                throw new ArgumentNullException();

            int length = values.Length;
            if (length == 0)
                return 0;

            short num2 = value;
            for (int i = 0; i < length; i++)
            {
                if (values[i] > num2)
                    num2 = values[i];
            }

            return num2;
        }

        public static ushort Max(this ushort value, params ushort[] values)
        {
            if (values == null)
                throw new ArgumentNullException();

            int length = values.Length;
            if (length == 0)
                return 0;

            ushort num2 = value;
            for (int i = 0; i < length; i++)
            {
                if (values[i] > num2)
                    num2 = values[i];
            }

            return num2;
        }

        public static int Max(this int value, params int[] values)
        {
            if (values == null)
                throw new ArgumentNullException();

            int length = values.Length;
            if (length == 0)
                return 0;

            int num2 = value;
            for (int i = 0; i < length; i++)
            {
                if (values[i] > num2)
                    num2 = values[i];
            }

            return num2;
        }

        public static uint Max(this uint value, params uint[] values)
        {
            if (values == null)
                throw new ArgumentNullException();

            int length = values.Length;
            if (length == 0)
                return 0;

            uint num2 = value;
            for (int i = 0; i < length; i++)
            {
                if (values[i] > num2)
                    num2 = values[i];
            }

            return num2;
        }

        public static long Max(this long value, params long[] values)
        {
            if (values == null)
                throw new ArgumentNullException();

            int length = values.Length;
            if (length == 0)
                return 0;

            long num2 = value;
            for (int i = 0; i < length; i++)
            {
                if (values[i] > num2)
                    num2 = values[i];
            }

            return num2;
        }

        public static ulong Max(this ulong value, params ulong[] values)
        {
            if (values == null)
                throw new ArgumentNullException();

            int length = values.Length;
            if (length == 0)
                return 0;

            ulong num2 = value;
            for (int i = 0; i < length; i++)
            {
                if (values[i] > num2)
                    num2 = values[i];
            }

            return num2;
        }

        public static float Max(this float value, params float[] values)
        {
            if (values == null)
                throw new ArgumentNullException();

            int length = values.Length;
            if (length == 0)
                return 0;

            float num2 = value;
            for (int i = 0; i < length; i++)
            {
                if (values[i] > num2)
                    num2 = values[i];
            }

            return num2;
        }

        public static double Max(this double value, params double[] values)
        {
            if (values == null)
                throw new ArgumentNullException();

            int length = values.Length;
            if (length == 0)
                return 0;

            double num2 = value;
            for (int i = 0; i < length; i++)
            {
                if (values[i] > num2)
                    num2 = values[i];
            }

            return num2;
        }

        public static decimal Max(this decimal value, params decimal[] values)
        {
            if (values == null)
                throw new ArgumentNullException();

            int length = values.Length;
            if (length == 0)
                return 0;

            decimal num2 = value;
            for (int i = 0; i < length; i++)
            {
                if (values[i] > num2)
                    num2 = values[i];
            }

            return num2;
        }

        public static BigInteger Max(this BigInteger value, params BigInteger[] values)
        {
            if (values == null)
                throw new ArgumentNullException();

            int length = values.Length;
            if (length == 0)
                return 0;

            BigInteger num2 = value;
            for (int i = 0; i < length; i++)
            {
                if (values[i] > num2)
                    num2 = values[i];
            }

            return num2;
        }

        public static BigDecimal Max(this BigDecimal value, params BigDecimal[] values)
        {
            if (values == null)
                throw new ArgumentNullException();

            int length = values.Length;
            if (length == 0)
                return 0;

            BigDecimal num2 = value;
            for (int i = 0; i < length; i++)
            {
                if (values[i] > num2)
                    num2 = values[i];
            }

            return num2;
        }

        public static nint Max(this nint value, params nint[] values)
        {
            if (values == null)
                throw new ArgumentNullException();

            int length = values.Length;
            if (length == 0)
                return 0;

            nint num2 = value;
            for (int i = 0; i < length; i++)
            {
                if (values[i] > num2)
                    num2 = values[i];
            }

            return num2;
        }

        public static nuint Max(this nuint value, params nuint[] values)
        {
            if (values == null)
                throw new ArgumentNullException();

            int length = values.Length;
            if (length == 0)
                return 0;

            nuint num2 = value;
            for (int i = 0; i < length; i++)
            {
                if (values[i] > num2)
                    num2 = values[i];
            }

            return num2;
        }
        #endregion
    }

    public static class ListTool
    {
        public static void Move<T>(this List<T> list, int oldIndex, int newIndex)
        {
            if (list == null)
                throw new ArgumentNullException(nameof(list));

            T temp = list[oldIndex];
            list.RemoveAt(oldIndex);
            list.Insert(newIndex, temp);
        }

        public static void Change<T>(this List<T> list, int oldIndex, int newIndex)
        {
            if (list == null)
                throw new ArgumentNullException(nameof(list));

            T temp = list[newIndex];
            list[newIndex] = list[oldIndex];
            list[oldIndex] = temp;
        }

        #region Close Value
        /// <summary>
        /// ?????? ????????? ?????? ????????????
        /// </summary>
        /// <param name="list">?????????</param>
        /// <param name="target">??????</param>
        /// <returns></returns>
        public static byte CloseValue(this List<byte> list, byte target)
        {
            if (list == null)
                throw new ArgumentNullException(nameof(list));

            if (list.Count > 0)
                return list.Aggregate((x, y) => (x - target) < (y - target) ? x : y);

            return 0;
        }

        /// <summary>
        /// ?????? ????????? ?????? ????????????
        /// </summary>
        /// <param name="list">?????????</param>
        /// <param name="target">??????</param>
        /// <returns></returns>
        public static sbyte CloseValue(this List<sbyte> list, sbyte target)
        {
            if (list == null)
                throw new ArgumentNullException(nameof(list));

            if (list.Count > 0)
                return list.Aggregate((x, y) => (x - target).Abs() < (y - target).Abs() ? x : y);

            return 0;
        }

        /// <summary>
        /// ?????? ????????? ?????? ????????????
        /// </summary>
        /// <param name="list">?????????</param>
        /// <param name="target">??????</param>
        /// <returns></returns>
        public static short CloseValue(this List<short> list, short target)
        {
            if (list == null)
                throw new ArgumentNullException(nameof(list));

            if (list.Count > 0)
                return list.Aggregate((x, y) => (x - target).Abs() < (y - target).Abs() ? x : y);

            return 0;
        }

        /// <summary>
        /// ?????? ????????? ?????? ????????????
        /// </summary>
        /// <param name="list">?????????</param>
        /// <param name="target">??????</param>
        /// <returns></returns>
        public static ushort CloseValue(this List<ushort> list, ushort target)
        {
            if (list == null)
                throw new ArgumentNullException(nameof(list));

            if (list.Count > 0)
                return list.Aggregate((x, y) => (x - target) < (y - target) ? x : y);

            return 0;
        }

        /// <summary>
        /// ?????? ????????? ?????? ????????????
        /// </summary>
        /// <param name="list">?????????</param>
        /// <param name="target">??????</param>
        /// <returns></returns>
        public static int CloseValue(this List<int> list, int target)
        {
            if (list == null)
                throw new ArgumentNullException(nameof(list));

            if (list.Count > 0)
                return list.Aggregate((x, y) => (x - target).Abs() < (y - target).Abs() ? x : y);

            return 0;
        }

        /// <summary>
        /// ?????? ????????? ?????? ????????????
        /// </summary>
        /// <param name="list">?????????</param>
        /// <param name="target">??????</param>
        /// <returns></returns>
        public static uint CloseValue(this List<uint> list, uint target)
        {
            if (list == null)
                throw new ArgumentNullException(nameof(list));

            if (list.Count > 0)
                return list.Aggregate((x, y) => (x - target) < (y - target) ? x : y);

            return 0;
        }

        /// <summary>
        /// ?????? ????????? ?????? ????????????
        /// </summary>
        /// <param name="list">?????????</param>
        /// <param name="target">??????</param>
        /// <returns></returns>
        public static long CloseValue(this List<long> list, long target)
        {
            if (list == null)
                throw new ArgumentNullException(nameof(list));

            if (list.Count > 0)
                return list.Aggregate((x, y) => (x - target).Abs() < (y - target).Abs() ? x : y);

            return 0;
        }

        /// <summary>
        /// ?????? ????????? ?????? ????????????
        /// </summary>
        /// <param name="list">?????????</param>
        /// <param name="target">??????</param>
        /// <returns></returns>
        public static ulong CloseValue(this List<ulong> list, ulong target)
        {
            if (list == null)
                throw new ArgumentNullException(nameof(list));

            if (list.Count > 0)
                return list.Aggregate((x, y) => (x - target) < (y - target) ? x : y);

            return 0;
        }

        /// <summary>
        /// ?????? ????????? ?????? ????????????
        /// </summary>
        /// <param name="list">?????????</param>
        /// <param name="target">??????</param>
        /// <returns></returns>
        public static float CloseValue(this List<float> list, float target)
        {
            if (list == null)
                throw new ArgumentNullException(nameof(list));

            if (list.Count > 0)
                return list.Aggregate((x, y) => (x - target).Abs() < (y - target).Abs() ? x : y);

            return 0;
        }

        /// <summary>
        /// ?????? ????????? ?????? ????????????
        /// </summary>
        /// <param name="list">?????????</param>
        /// <param name="target">??????</param>
        /// <returns></returns>
        public static double CloseValue(this List<double> list, double target)
        {
            if (list == null)
                throw new ArgumentNullException(nameof(list));

            if (list.Count > 0)
                return list.Aggregate((x, y) => (x - target).Abs() < (y - target).Abs() ? x : y);

            return 0;
        }

        /// <summary>
        /// ?????? ????????? ?????? ????????????
        /// </summary>
        /// <param name="list">?????????</param>
        /// <param name="target">??????</param>
        /// <returns></returns>
        public static decimal CloseValue(this List<decimal> list, decimal target)
        {
            if (list == null)
                throw new ArgumentNullException(nameof(list));

            if (list.Count > 0)
                return list.Aggregate((x, y) => (x - target).Abs() < (y - target).Abs() ? x : y);

            return 0;
        }

        /// <summary>
        /// ?????? ????????? ?????? ????????????
        /// </summary>
        /// <param name="list">?????????</param>
        /// <param name="target">??????</param>
        /// <returns></returns>
        public static BigInteger CloseValue(this List<BigInteger> list, BigInteger target)
        {
            if (list == null)
                throw new ArgumentNullException(nameof(list));

            if (list.Count > 0)
                return list.Aggregate((x, y) => (x - target).Abs() < (y - target).Abs() ? x : y);

            return 0;
        }

        /// <summary>
        /// ?????? ????????? ?????? ????????????
        /// </summary>
        /// <param name="list">?????????</param>
        /// <param name="target">??????</param>
        /// <returns></returns>
        public static BigDecimal CloseValue(this List<BigDecimal> list, BigDecimal target)
        {
            if (list == null)
                throw new ArgumentNullException(nameof(list));

            if (list.Count > 0)
                return list.Aggregate((x, y) => (x - target).Abs() < (y - target).Abs() ? x : y);

            return 0;
        }

        /// <summary>
        /// ?????? ????????? ?????? ????????????
        /// </summary>
        /// <param name="list">?????????</param>
        /// <param name="target">??????</param>
        /// <returns></returns>
        public static nint CloseValue(this List<nint> list, nint target)
        {
            if (list == null)
                throw new ArgumentNullException(nameof(list));

            if (list.Count > 0)
                return list.Aggregate((x, y) => (x - target) < (y - target) ? x : y);

            return 0;
        }

        /// <summary>
        /// ?????? ????????? ?????? ????????????
        /// </summary>
        /// <param name="list">?????????</param>
        /// <param name="target">??????</param>
        /// <returns></returns>
        public static nuint CloseValue(this List<nuint> list, nuint target)
        {
            if (list == null)
                throw new ArgumentNullException(nameof(list));

            if (list.Count > 0)
                return list.Aggregate((x, y) => (x - target) < (y - target) ? x : y);

            return 0;
        }
        #endregion

        #region Close Value Index
        /// <summary>
        /// ?????? ????????? ?????? ?????? ???????????? ???????????????
        /// </summary>
        /// <param name="list"></param>
        /// <param name="target"></param>
        /// <returns></returns>
        public static int CloseValueIndex(this List<byte> list, byte target)
        {
            if (list == null)
                throw new ArgumentNullException(nameof(list));

            if (list.Count > 0)
                return list.IndexOf(list.CloseValue(target));

            return 0;
        }

        /// <summary>
        /// ?????? ????????? ?????? ?????? ???????????? ???????????????
        /// </summary>
        /// <param name="list"></param>
        /// <param name="target"></param>
        /// <returns></returns>
        public static int CloseValueIndex(this List<sbyte> list, sbyte target)
        {
            if (list == null)
                throw new ArgumentNullException(nameof(list));

            if (list.Count > 0)
                return list.IndexOf(list.CloseValue(target));

            return 0;
        }

        /// <summary>
        /// ?????? ????????? ?????? ?????? ???????????? ???????????????
        /// </summary>
        /// <param name="list"></param>
        /// <param name="target"></param>
        /// <returns></returns>
        public static int CloseValueIndex(this List<short> list, short target)
        {
            if (list == null)
                throw new ArgumentNullException(nameof(list));

            if (list.Count > 0)
                return list.IndexOf(list.CloseValue(target));

            return 0;
        }

        /// <summary>
        /// ?????? ????????? ?????? ?????? ???????????? ???????????????
        /// </summary>
        /// <param name="list"></param>
        /// <param name="target"></param>
        /// <returns></returns>
        public static int CloseValueIndex(this List<ushort> list, ushort target)
        {
            if (list == null)
                throw new ArgumentNullException(nameof(list));

            if (list.Count > 0)
                return list.IndexOf(list.CloseValue(target));

            return 0;
        }

        /// <summary>
        /// ?????? ????????? ?????? ?????? ???????????? ???????????????
        /// </summary>
        /// <param name="list"></param>
        /// <param name="target"></param>
        /// <returns></returns>
        public static int CloseValueIndex(this List<int> list, int target)
        {
            if (list == null)
                throw new ArgumentNullException(nameof(list));

            if (list.Count > 0)
                return list.IndexOf(list.CloseValue(target));

            return 0;
        }

        /// <summary>
        /// ?????? ????????? ?????? ?????? ???????????? ???????????????
        /// </summary>
        /// <param name="list"></param>
        /// <param name="target"></param>
        /// <returns></returns>
        public static int CloseValueIndex(this List<uint> list, uint target)
        {
            if (list == null)
                throw new ArgumentNullException(nameof(list));

            if (list.Count > 0)
                return list.IndexOf(list.CloseValue(target));

            return 0;
        }

        /// <summary>
        /// ?????? ????????? ?????? ?????? ???????????? ???????????????
        /// </summary>
        /// <param name="list"></param>
        /// <param name="target"></param>
        /// <returns></returns>
        public static int CloseValueIndex(this List<long> list, long target)
        {
            if (list == null)
                throw new ArgumentNullException(nameof(list));

            if (list.Count > 0)
                return list.IndexOf(list.CloseValue(target));

            return 0;
        }

        /// <summary>
        /// ?????? ????????? ?????? ?????? ???????????? ???????????????
        /// </summary>
        /// <param name="list"></param>
        /// <param name="target"></param>
        /// <returns></returns>
        public static int CloseValueIndex(this List<ulong> list, ulong target)
        {
            if (list == null)
                throw new ArgumentNullException(nameof(list));

            if (list.Count > 0)
                return list.IndexOf(list.CloseValue(target));

            return 0;
        }

        /// <summary>
        /// ?????? ????????? ?????? ?????? ???????????? ???????????????
        /// </summary>
        /// <param name="list"></param>
        /// <param name="target"></param>
        /// <returns></returns>
        public static int CloseValueIndex(this List<float> list, float target)
        {
            if (list == null)
                throw new ArgumentNullException(nameof(list));

            if (list.Count > 0)
                return list.IndexOf(list.CloseValue(target));

            return 0;
        }

        /// <summary>
        /// ?????? ????????? ?????? ?????? ???????????? ???????????????
        /// </summary>
        /// <param name="list"></param>
        /// <param name="target"></param>
        /// <returns></returns>
        public static int CloseValueIndex(this List<double> list, double target)
        {
            if (list == null)
                throw new ArgumentNullException(nameof(list));

            if (list.Count > 0)
                return list.IndexOf(list.CloseValue(target));

            return 0;
        }

        /// <summary>
        /// ?????? ????????? ?????? ?????? ???????????? ???????????????
        /// </summary>
        /// <param name="list"></param>
        /// <param name="target"></param>
        /// <returns></returns>
        public static int CloseValueIndex(this List<decimal> list, decimal target)
        {
            if (list == null)
                throw new ArgumentNullException(nameof(list));

            if (list.Count > 0)
                return list.IndexOf(list.CloseValue(target));

            return 0;
        }

        /// <summary>
        /// ?????? ????????? ?????? ?????? ???????????? ???????????????
        /// </summary>
        /// <param name="list"></param>
        /// <param name="target"></param>
        /// <returns></returns>
        public static int CloseValueIndex(this List<BigInteger> list, BigInteger target)
        {
            if (list == null)
                throw new ArgumentNullException(nameof(list));

            if (list.Count > 0)
                return list.IndexOf(list.CloseValue(target));

            return 0;
        }

        /// <summary>
        /// ?????? ????????? ?????? ?????? ???????????? ???????????????
        /// </summary>
        /// <param name="list"></param>
        /// <param name="target"></param>
        /// <returns></returns>
        public static int CloseValueIndex(this List<BigDecimal> list, BigDecimal target)
        {
            if (list == null)
                throw new ArgumentNullException(nameof(list));

            if (list.Count > 0)
                return list.IndexOf(list.CloseValue(target));

            return 0;
        }

        /// <summary>
        /// ?????? ????????? ?????? ?????? ???????????? ???????????????
        /// </summary>
        /// <param name="list"></param>
        /// <param name="target"></param>
        /// <returns></returns>
        public static int CloseValueIndex(this List<nint> list, nint target)
        {
            if (list == null)
                throw new ArgumentNullException(nameof(list));

            if (list.Count > 0)
                return list.IndexOf(list.CloseValue(target));

            return 0;
        }

        /// <summary>
        /// ?????? ????????? ?????? ?????? ???????????? ???????????????
        /// </summary>
        /// <param name="list"></param>
        /// <param name="target"></param>
        /// <returns></returns>
        public static int CloseValueIndex(this List<nuint> list, nuint target)
        {
            if (list == null)
                throw new ArgumentNullException(nameof(list));

            if (list.Count > 0)
                return list.IndexOf(list.CloseValue(target));

            return 0;
        }
        #endregion

        #region Close Value Index Binary Search
        /// <summary>
        /// ?????? ????????? ?????? ?????? ?????? ???????????? ???????????? ???????????????
        /// </summary>
        /// <param name="list"></param>
        /// <param name="target"></param>
        /// <returns></returns>
        public static int CloseValueIndexBinarySearch(this List<byte> list, byte target)
        {
            if (list == null)
                throw new ArgumentNullException(nameof(list));

            if (list.Count > 0)
                return list.IndexOf(list.CloseValue(target));

            return 0;
        }

        /// <summary>
        /// ?????? ????????? ?????? ?????? ?????? ???????????? ???????????? ???????????????
        /// </summary>
        /// <param name="list"></param>
        /// <param name="target"></param>
        /// <returns></returns>
        public static int CloseValueIndexBinarySearch(this List<sbyte> list, sbyte target)
        {
            if (list == null)
                throw new ArgumentNullException(nameof(list));

            if (list.Count > 0)
                return list.IndexOf(list.CloseValue(target));

            return 0;
        }

        /// <summary>
        /// ?????? ????????? ?????? ?????? ?????? ???????????? ???????????? ???????????????
        /// </summary>
        /// <param name="list"></param>
        /// <param name="target"></param>
        /// <returns></returns>
        public static int CloseValueIndexBinarySearch(this List<short> list, short target)
        {
            if (list == null)
                throw new ArgumentNullException(nameof(list));

            if (list.Count > 0)
                return list.IndexOf(list.CloseValue(target));

            return 0;
        }

        /// <summary>
        /// ?????? ????????? ?????? ?????? ?????? ???????????? ???????????? ???????????????
        /// </summary>
        /// <param name="list"></param>
        /// <param name="target"></param>
        /// <returns></returns>
        public static int CloseValueIndexBinarySearch(this List<ushort> list, ushort target)
        {
            if (list == null)
                throw new ArgumentNullException(nameof(list));

            if (list.Count > 0)
                return list.IndexOf(list.CloseValue(target));

            return 0;
        }

        /// <summary>
        /// ?????? ????????? ?????? ?????? ?????? ???????????? ???????????? ???????????????
        /// </summary>
        /// <param name="list"></param>
        /// <param name="target"></param>
        /// <returns></returns>
        public static int CloseValueIndexBinarySearch(this List<int> list, int target)
        {
            if (list == null)
                throw new ArgumentNullException(nameof(list));

            if (list.Count > 0)
                return list.IndexOf(list.CloseValue(target));

            return 0;
        }

        /// <summary>
        /// ?????? ????????? ?????? ?????? ?????? ???????????? ???????????? ???????????????
        /// </summary>
        /// <param name="list"></param>
        /// <param name="target"></param>
        /// <returns></returns>
        public static int CloseValueIndexBinarySearch(this List<uint> list, uint target)
        {
            if (list == null)
                throw new ArgumentNullException(nameof(list));

            if (list.Count > 0)
                return list.IndexOf(list.CloseValue(target));

            return 0;
        }

        /// <summary>
        /// ?????? ????????? ?????? ?????? ?????? ???????????? ???????????? ???????????????
        /// </summary>
        /// <param name="list"></param>
        /// <param name="target"></param>
        /// <returns></returns>
        public static int CloseValueIndexBinarySearch(this List<long> list, long target)
        {
            if (list == null)
                throw new ArgumentNullException(nameof(list));

            if (list.Count > 0)
                return list.IndexOf(list.CloseValue(target));

            return 0;
        }

        /// <summary>
        /// ?????? ????????? ?????? ?????? ?????? ???????????? ???????????? ???????????????
        /// </summary>
        /// <param name="list"></param>
        /// <param name="target"></param>
        /// <returns></returns>
        public static int CloseValueIndexBinarySearch(this List<ulong> list, ulong target)
        {
            if (list == null)
                throw new ArgumentNullException(nameof(list));

            if (list.Count > 0)
                return list.IndexOf(list.CloseValue(target));

            return 0;
        }

        /// <summary>
        /// ?????? ????????? ?????? ?????? ?????? ???????????? ???????????? ???????????????
        /// </summary>
        /// <param name="list"></param>
        /// <param name="target"></param>
        /// <returns></returns>
        public static int CloseValueIndexBinarySearch(this List<float> list, float target)
        {
            if (list == null)
                throw new ArgumentNullException(nameof(list));

            if (list.Count > 0)
                return list.IndexOf(list.CloseValue(target));

            return 0;
        }

        /// <summary>
        /// ?????? ????????? ?????? ?????? ?????? ???????????? ???????????? ???????????????
        /// </summary>
        /// <param name="list"></param>
        /// <param name="target"></param>
        /// <returns></returns>
        public static int CloseValueIndexBinarySearch(this List<double> list, double target)
        {
            if (list == null)
                throw new ArgumentNullException(nameof(list));

            if (list.Count > 0)
                return list.IndexOf(list.CloseValue(target));

            return 0;
        }

        /// <summary>
        /// ?????? ????????? ?????? ?????? ?????? ???????????? ???????????? ???????????????
        /// </summary>
        /// <param name="list"></param>
        /// <param name="target"></param>
        /// <returns></returns>
        public static int CloseValueIndexBinarySearch(this List<decimal> list, decimal target)
        {
            if (list == null)
                throw new ArgumentNullException(nameof(list));

            if (list.Count > 0)
                return list.IndexOf(list.CloseValue(target));

            return 0;
        }

        /// <summary>
        /// ?????? ????????? ?????? ?????? ?????? ???????????? ???????????? ???????????????
        /// </summary>
        /// <param name="list"></param>
        /// <param name="target"></param>
        /// <returns></returns>
        public static int CloseValueIndexBinarySearch(this List<BigInteger> list, BigInteger target)
        {
            if (list == null)
                throw new ArgumentNullException(nameof(list));

            if (list.Count > 0)
                return list.IndexOf(list.CloseValue(target));

            return 0;
        }

        /// <summary>
        /// ?????? ????????? ?????? ?????? ?????? ???????????? ???????????? ???????????????
        /// </summary>
        /// <param name="list"></param>
        /// <param name="target"></param>
        /// <returns></returns>
        public static int CloseValueIndexBinarySearch(this List<BigDecimal> list, BigDecimal target)
        {
            if (list == null)
                throw new ArgumentNullException(nameof(list));

            if (list.Count > 0)
                return list.IndexOf(list.CloseValue(target));

            return 0;
        }

        /// <summary>
        /// ?????? ????????? ?????? ?????? ?????? ???????????? ???????????? ???????????????
        /// </summary>
        /// <param name="list"></param>
        /// <param name="target"></param>
        /// <returns></returns>
        public static int CloseValueIndexBinarySearch(this List<nint> list, nint target)
        {
            if (list == null)
                throw new ArgumentNullException(nameof(list));

            if (list.Count > 0)
                return list.IndexOf(list.CloseValue(target));

            return 0;
        }

        /// <summary>
        /// ?????? ????????? ?????? ?????? ?????? ???????????? ???????????? ???????????????
        /// </summary>
        /// <param name="list"></param>
        /// <param name="target"></param>
        /// <returns></returns>
        public static int CloseValueIndexBinarySearch(this List<nuint> list, nuint target)
        {
            if (list == null)
                throw new ArgumentNullException(nameof(list));

            if (list.Count > 0)
                return list.IndexOf(list.CloseValue(target));

            return 0;
        }
        #endregion

        public static TSource MinBy<TSource, TKey>(this IEnumerable<TSource> source, Func<TSource, TKey> selector)
        {
            if (source == null)
                throw new ArgumentNullException(nameof(source));

            using (IEnumerator<TSource> sourceIterator = source.GetEnumerator())
            {
                if (!sourceIterator.MoveNext())
                    throw new InvalidOperationException("Empty sequence");

                var comparer = Comparer<TKey>.Default;
                TSource min = sourceIterator.Current;
                TKey minKey = selector(min);

                while (sourceIterator.MoveNext())
                {
                    TSource current = sourceIterator.Current;
                    TKey currentKey = selector(current);

                    if (comparer.Compare(currentKey, minKey) >= 0)
                        continue;

                    min = current;
                    minKey = currentKey;
                }

                return min;
            }
        }

        #region Deduplicate
        public static void Deduplicate(this List<float> values, float delta)
        {
            int index = 0;
            while (index < values.Count)
            {
                float value = values[index];

                int i = index + 1;
                while (i < values.Count)
                {
                    if ((value - values[i]).Abs() < delta)
                        values.RemoveAt(i);
                    else
                        i++;
                }

                index++;
            }
        }

        public static void Deduplicate(this List<float> values, float delta, float setValue)
        {
            int index = 0;
            while (index < values.Count)
            {
                float value = values[index];

                int i = index + 1;
                while (i < values.Count)
                {
                    if ((value - values[i]).Abs() < delta)
                        values[i] = setValue;

                    i++;
                }

                index++;
            }
        }

        public static void Deduplicate(this List<double> values, double delta)
        {
            int index = 0;
            while (index < values.Count)
            {
                double value = values[index];

                int i = index + 1;
                while (i < values.Count)
                {
                    if ((value - values[i]).Abs() < delta)
                        values.RemoveAt(i);
                    else
                        i++;
                }

                index++;
            }
        }

        public static void Deduplicate(this List<double> values, double delta, double setValue)
        {
            int index = 0;
            while (index < values.Count)
            {
                double value = values[index];

                int i = index + 1;
                while (i < values.Count)
                {
                    if ((value - values[i]).Abs() < delta)
                        values[i] = setValue;

                    i++;
                }

                index++;
            }
        }

        public static void Deduplicate(this List<decimal> values, decimal delta)
        {
            int index = 0;
            while (index < values.Count)
            {
                decimal value = values[index];

                int i = index + 1;
                while (i < values.Count)
                {
                    if ((value - values[i]).Abs() < delta)
                        values.RemoveAt(i);
                    else
                        i++;
                }

                index++;
            }
        }

        public static void Deduplicate(this List<decimal> values, decimal delta, decimal setValue)
        {
            int index = 0;
            while (index < values.Count)
            {
                decimal value = values[index];

                int i = index + 1;
                while (i < values.Count)
                {
                    if ((value - values[i]).Abs() < delta)
                        values[i] = setValue;

                    i++;
                }

                index++;
            }
        }

        public static void Deduplicate(this List<BigDecimal> values, BigDecimal delta)
        {
            int index = 0;
            while (index < values.Count)
            {
                BigDecimal value = values[index];

                int i = index + 1;
                while (i < values.Count)
                {
                    if ((value - values[i]).Abs() < delta)
                        values.RemoveAt(i);
                    else
                        i++;
                }

                index++;
            }
        }

        public static void Deduplicate(this List<BigDecimal> values, BigDecimal delta, BigDecimal setValue)
        {
            int index = 0;
            while (index < values.Count)
            {
                BigDecimal value = values[index];

                int i = index + 1;
                while (i < values.Count)
                {
                    if ((value - values[i]).Abs() < delta)
                        values[i] = setValue;

                    i++;
                }

                index++;
            }
        }
        #endregion
    }

    public static class ArrayTool
    {
        #region Close Value
        /// <summary>
        /// ?????? ????????? ?????? ????????????
        /// </summary>
        /// <param name="array">
        /// ?????????
        /// </param>
        /// <param name="target">
        /// ??????
        /// </param>
        /// <returns>
        /// byte
        /// </returns>
        public static byte CloseValue(this byte[] array, byte target)
        {
            if (array == null)
                throw new ArgumentNullException(nameof(array));

            if (array.Length > 0)
                return array.Aggregate((x, y) => (x - target) < (y - target) ? x : y);

            return 0;
        }

        /// <summary>
        /// ?????? ????????? ?????? ????????????
        /// </summary>
        /// <param name="array">
        /// ?????????
        /// </param>
        /// <param name="target">
        /// ??????
        /// </param>
        /// <returns>
        /// sbyte
        /// </returns>
        public static sbyte CloseValue(this sbyte[] array, sbyte target)
        {
            if (array == null)
                throw new ArgumentNullException(nameof(array));

            if (array.Length > 0)
                return array.Aggregate((x, y) => (x - target).Abs() < (y - target).Abs() ? x : y);

            return 0;
        }

        /// <summary>
        /// ?????? ????????? ?????? ????????????
        /// </summary>
        /// <param name="array">
        /// ?????????
        /// </param>
        /// <param name="target">
        /// ??????
        /// </param>
        /// <returns>
        /// short
        /// </returns>
        public static short CloseValue(this short[] array, short target)
        {
            if (array == null)
                throw new ArgumentNullException(nameof(array));

            if (array.Length > 0)
                return array.Aggregate((x, y) => (x - target).Abs() < (y - target).Abs() ? x : y);

            return 0;
        }

        /// <summary>
        /// ?????? ????????? ?????? ????????????
        /// </summary>
        /// <param name="array">
        /// ?????????
        /// </param>
        /// <param name="target">
        /// ??????
        /// </param>
        /// <returns>
        /// ushort
        /// </returns>
        public static ushort CloseValue(this ushort[] array, ushort target)
        {
            if (array == null)
                throw new ArgumentNullException(nameof(array));

            if (array.Length > 0)
                return array.Aggregate((x, y) => (x - target) < (y - target) ? x : y);

            return 0;
        }

        /// <summary>
        /// ?????? ????????? ?????? ????????????
        /// </summary>
        /// <param name="array">
        /// ?????????
        /// </param>
        /// <param name="target">
        /// ??????
        /// </param>
        /// <returns>
        /// int
        /// </returns>
        public static int CloseValue(this int[] array, int target)
        {
            if (array == null)
                throw new ArgumentNullException(nameof(array));

            if (array.Length > 0)
                return array.Aggregate((x, y) => (x - target).Abs() < (y - target).Abs() ? x : y);

            return 0;
        }

        /// <summary>
        /// ?????? ????????? ?????? ????????????
        /// </summary>
        /// <param name="array">
        /// ?????????
        /// </param>
        /// <param name="target">
        /// ??????
        /// </param>
        /// <returns>
        /// uint
        /// </returns>
        public static uint CloseValue(this uint[] array, uint target)
        {
            if (array == null)
                throw new ArgumentNullException(nameof(array));

            if (array.Length > 0)
                return array.Aggregate((x, y) => (x - target) < (y - target) ? x : y);

            return 0;
        }

        /// <summary>
        /// ?????? ????????? ?????? ????????????
        /// </summary>
        /// <param name="array">
        /// ?????????
        /// </param>
        /// <param name="target">
        /// ??????
        /// </param>
        /// <returns>
        /// long
        /// </returns>
        public static long CloseValue(this long[] array, long target)
        {
            if (array == null)
                throw new ArgumentNullException(nameof(array));

            if (array.Length > 0)
                return array.Aggregate((x, y) => (x - target) < (y - target) ? x : y);

            return 0;
        }

        /// <summary>
        /// ?????? ????????? ?????? ????????????
        /// </summary>
        /// <param name="array">
        /// ?????????
        /// </param>
        /// <param name="target">
        /// ??????
        /// </param>
        /// <returns>
        /// ulong
        /// </returns>
        public static ulong CloseValue(this ulong[] array, ulong target)
        {
            if (array == null)
                throw new ArgumentNullException(nameof(array));

            if (array.Length > 0)
                return array.Aggregate((x, y) => (x - target) < (y - target) ? x : y);

            return 0;
        }

        /// <summary>
        /// ?????? ????????? ?????? ????????????
        /// </summary>
        /// <param name="array">
        /// ?????????
        /// </param>
        /// <param name="target">
        /// ??????
        /// </param>
        /// <returns>
        /// float
        /// </returns>
        public static float CloseValue(this float[] array, float target)
        {
            if (array == null)
                throw new ArgumentNullException(nameof(array));

            if (array.Length > 0)
                return array.Aggregate((x, y) => (x - target).Abs() < (y - target).Abs() ? x : y);

            return 0;
        }

        /// <summary>
        /// ?????? ????????? ?????? ????????????
        /// </summary>
        /// <param name="array">
        /// ?????????
        /// </param>
        /// <param name="target">
        /// ??????
        /// </param>
        /// <returns>
        /// double
        /// </returns>
        public static double CloseValue(this double[] array, double target)
        {
            if (array == null)
                throw new ArgumentNullException(nameof(array));

            if (array.Length > 0)
                return array.Aggregate((x, y) => (x - target).Abs() < (y - target).Abs() ? x : y);

            return 0;
        }

        /// <summary>
        /// ?????? ????????? ?????? ????????????
        /// </summary>
        /// <param name="array">
        /// ?????????
        /// </param>
        /// <param name="target">
        /// ??????
        /// </param>
        /// <returns>
        /// double
        /// </returns>
        public static decimal CloseValue(this decimal[] array, decimal target)
        {
            if (array == null)
                throw new ArgumentNullException(nameof(array));

            if (array.Length > 0)
                return array.Aggregate((x, y) => (x - target).Abs() < (y - target).Abs() ? x : y);

            return 0;
        }

        /// <summary>
        /// ?????? ????????? ?????? ????????????
        /// </summary>
        /// <param name="array">
        /// ?????????
        /// </param>
        /// <param name="target">
        /// ??????
        /// </param>
        /// <returns>
        /// double
        /// </returns>
        public static BigInteger CloseValue(this BigInteger[] array, BigInteger target)
        {
            if (array == null)
                throw new ArgumentNullException(nameof(array));

            if (array.Length > 0)
                return array.Aggregate((x, y) => (x - target).Abs() < (y - target).Abs() ? x : y);

            return 0;
        }

        /// <summary>
        /// ?????? ????????? ?????? ????????????
        /// </summary>
        /// <param name="array">
        /// ?????????
        /// </param>
        /// <param name="target">
        /// ??????
        /// </param>
        /// <returns>
        /// double
        /// </returns>
        public static BigDecimal CloseValue(this BigDecimal[] array, BigDecimal target)
        {
            if (array == null)
                throw new ArgumentNullException(nameof(array));

            if (array.Length > 0)
                return array.Aggregate((x, y) => (x - target).Abs() < (y - target).Abs() ? x : y);

            return 0;
        }

        /// <summary>
        /// ?????? ????????? ?????? ????????????
        /// </summary>
        /// <param name="array">
        /// ?????????
        /// </param>
        /// <param name="target">
        /// ??????
        /// </param>
        /// <returns>
        /// double
        /// </returns>
        public static nint CloseValue(this nint[] array, nint target)
        {
            if (array == null)
                throw new ArgumentNullException(nameof(array));

            if (array.Length > 0)
                return array.Aggregate((x, y) => (x - target).Abs() < (y - target).Abs() ? x : y);

            return 0;
        }

        /// <summary>
        /// ?????? ????????? ?????? ????????????
        /// </summary>
        /// <param name="array">
        /// ?????????
        /// </param>
        /// <param name="target">
        /// ??????
        /// </param>
        /// <returns>
        /// double
        /// </returns>
        public static nuint CloseValue(this nuint[] array, nuint target)
        {
            if (array == null)
                throw new ArgumentNullException(nameof(array));

            if (array.Length > 0)
                return array.Aggregate((x, y) => (x - target) < (y - target) ? x : y);

            return 0;
        }
        #endregion Close Value
    }

    public static class StringTool
    {
        public static string ConstEnvironmentVariable(this string value)
        {
            if (value == null)
                return null;

            value = value.Replace("%DataPath%", Kernel.dataPath);
            value = value.Replace("%StreamingAssetsPath%", Kernel.streamingAssetsPath);
            value = value.Replace("%PersistentDataPath%", Kernel.persistentDataPath);

            value = value.Replace("%CompanyName%", Kernel.companyName);
            value = value.Replace("%ProductName%", Kernel.productName);
            value = value.Replace("%Version%", Kernel.version);

            return value;
        }

        /// <summary>
        /// (text = "AddSpacesToSentence") = "Add Spaces To Sentence"
        /// </summary>
        /// <param name="text">?????????</param>
        /// <param name="preserveAcronyms">??????(??????) ?????? (true = (UnscaledFPSDeltaTime = Unscaled FPS Delta Time), false = (UnscaledFPSDeltaTime = Unscaled FPSDelta Time))</param>
        /// <returns></returns>
        public static string AddSpacesToSentence(this string text, bool preserveAcronyms = true)
        {
            if (string.IsNullOrWhiteSpace(text))
                return string.Empty;

            StringBuilder newText = new StringBuilder(text.Length * 2);
            newText.Append(text[0]);

            for (int i = 1; i < text.Length; i++)
            {
                if (char.IsUpper(text[i]))
                    if ((text[i - 1] != ' ' && !char.IsUpper(text[i - 1])) || (preserveAcronyms && char.IsUpper(text[i - 1]) && i < text.Length - 1 && !char.IsUpper(text[i + 1])))
                        newText.Append(' ');
                newText.Append(text[i]);
            }

            return newText.ToString();
        }

        #region KeyCode to String
        /// <summary>
        /// (keyCode = KeyCode.RightArrow) = "???"
        /// </summary>
        /// <param name="keyCode"></param>
        /// <returns></returns>
        public static string KeyCodeToString(this KeyCode keyCode, bool simply = false)
        {
            string text;
            switch (keyCode)
            {
                case KeyCode.Escape:
                    text = "ESC";
                    break;
                case KeyCode.Return when !simply:
                    text = "Enter";
                    break;
                case KeyCode.Return when simply:
                    text = "???";
                    break;
                case KeyCode.Alpha0:
                    text = "0";
                    break;
                case KeyCode.Alpha1:
                    text = "1";
                    break;
                case KeyCode.Alpha2:
                    text = "2";
                    break;
                case KeyCode.Alpha3:
                    text = "3";
                    break;
                case KeyCode.Alpha4:
                    text = "4";
                    break;
                case KeyCode.Alpha5:
                    text = "5";
                    break;
                case KeyCode.Alpha6:
                    text = "6";
                    break;
                case KeyCode.Alpha7:
                    text = "7";
                    break;
                case KeyCode.Alpha8:
                    text = "8";
                    break;
                case KeyCode.Alpha9:
                    text = "9";
                    break;
                case KeyCode.AltGr when simply:
                    text = "AG";
                    break;
                case KeyCode.Ampersand:
                    text = "&";
                    break;
                case KeyCode.Asterisk:
                    text = "*";
                    break;
                case KeyCode.At:
                    text = "@";
                    break;
                case KeyCode.BackQuote:
                    text = "`";
                    break;
                case KeyCode.Backslash:
                    text = "\\";
                    break;
                case KeyCode.Caret:
                    text = "^";
                    break;
                case KeyCode.Colon:
                    text = ":";
                    break;
                case KeyCode.Comma:
                    text = ",";
                    break;
                case KeyCode.Dollar:
                    text = "$";
                    break;
                case KeyCode.DoubleQuote:
                    text = "\"";
                    break;
                case KeyCode.Equals:
                    text = "=";
                    break;
                case KeyCode.Exclaim:
                    text = "!";
                    break;
                case KeyCode.Greater:
                    text = ">";
                    break;
                case KeyCode.Hash:
                    text = "#";
                    break;
                case KeyCode.Keypad0 when !simply:
                    text = "Keypad 0";
                    break;
                case KeyCode.Keypad1 when !simply:
                    text = "Keypad 1";
                    break;
                case KeyCode.Keypad2 when !simply:
                    text = "Keypad 2";
                    break;
                case KeyCode.Keypad3 when !simply:
                    text = "Keypad 3";
                    break;
                case KeyCode.Keypad4 when !simply:
                    text = "Keypad 4";
                    break;
                case KeyCode.Keypad5 when !simply:
                    text = "Keypad 5";
                    break;
                case KeyCode.Keypad6 when !simply:
                    text = "Keypad 6";
                    break;
                case KeyCode.Keypad7 when !simply:
                    text = "Keypad 7";
                    break;
                case KeyCode.Keypad8 when !simply:
                    text = "Keypad 8";
                    break;
                case KeyCode.Keypad9 when !simply:
                    text = "Keypad 9";
                    break;
                case KeyCode.KeypadDivide when !simply:
                    text = "Keypad /";
                    break;
                case KeyCode.KeypadEnter when !simply:
                    text = "Keypad ???";
                    break;
                case KeyCode.KeypadEquals when !simply:
                    text = "Keypad =";
                    break;
                case KeyCode.KeypadMinus when !simply:
                    text = "Keypad -";
                    break;
                case KeyCode.KeypadMultiply when !simply:
                    text = "Keypad *";
                    break;
                case KeyCode.KeypadPeriod when !simply:
                    text = "Keypad .";
                    break;
                case KeyCode.KeypadPlus when !simply:
                    text = "Keypad +";
                    break;
                case KeyCode.Keypad0 when simply:
                    text = "K0";
                    break;
                case KeyCode.Keypad1 when simply:
                    text = "K1";
                    break;
                case KeyCode.Keypad2 when simply:
                    text = "K2";
                    break;
                case KeyCode.Keypad3 when simply:
                    text = "K3";
                    break;
                case KeyCode.Keypad4 when simply:
                    text = "K4";
                    break;
                case KeyCode.Keypad5 when simply:
                    text = "K5";
                    break;
                case KeyCode.Keypad6 when simply:
                    text = "K6";
                    break;
                case KeyCode.Keypad7 when simply:
                    text = "K7";
                    break;
                case KeyCode.Keypad8 when simply:
                    text = "K8";
                    break;
                case KeyCode.Keypad9 when simply:
                    text = "K9";
                    break;
                case KeyCode.KeypadDivide when simply:
                    text = "K/";
                    break;
                case KeyCode.KeypadEnter when simply:
                    text = "K???";
                    break;
                case KeyCode.KeypadEquals when simply:
                    text = "K=";
                    break;
                case KeyCode.KeypadMinus when simply:
                    text = "K-";
                    break;
                case KeyCode.KeypadMultiply when simply:
                    text = "K*";
                    break;
                case KeyCode.KeypadPeriod when simply:
                    text = "K.";
                    break;
                case KeyCode.KeypadPlus when simply:
                    text = "K+";
                    break;
                case KeyCode.LeftApple:
                    text = "Left Command";
                    break;
                case KeyCode.LeftBracket:
                    text = "[";
                    break;
                case KeyCode.LeftCurlyBracket:
                    text = "{";
                    break;
                case KeyCode.LeftParen:
                    text = "(";
                    break;
                case KeyCode.LeftWindows when simply:
                    text = "LW";
                    break;
                case KeyCode.Less:
                    text = "<";
                    break;
                case KeyCode.Minus:
                    text = "-";
                    break;
                case KeyCode.Mouse0 when !simply:
                    text = "Left Mouse";
                    break;
                case KeyCode.Mouse1 when !simply:
                    text = "Right Mouse";
                    break;
                case KeyCode.Mouse2 when !simply:
                    text = "Middle Mouse";
                    break;
                case KeyCode.Mouse0 when simply:
                    text = "LM";
                    break;
                case KeyCode.Mouse1 when simply:
                    text = "RM";
                    break;
                case KeyCode.Mouse2 when simply:
                    text = "MM";
                    break;
                case KeyCode.Mouse3 when simply:
                    text = "M3";
                    break;
                case KeyCode.Mouse4 when simply:
                    text = "M4";
                    break;
                case KeyCode.Mouse5 when simply:
                    text = "M5";
                    break;
                case KeyCode.Mouse6 when simply:
                    text = "M6";
                    break;
                case KeyCode.Percent:
                    text = "%";
                    break;
                case KeyCode.Period:
                    text = ".";
                    break;
                case KeyCode.Pipe:
                    text = "|";
                    break;
                case KeyCode.Plus:
                    text = "+";
                    break;
                case KeyCode.Print when !simply:
                    text = "Print Screen";
                    break;
                case KeyCode.Print when simply:
                    text = "PS";
                    break;
                case KeyCode.Question:
                    text = "?";
                    break;
                case KeyCode.Quote:
                    text = "'";
                    break;
                case KeyCode.RightApple:
                    text = "Right Command";
                    break;
                case KeyCode.RightBracket:
                    text = "]";
                    break;
                case KeyCode.RightCurlyBracket:
                    text = "}";
                    break;
                case KeyCode.RightParen:
                    text = ")";
                    break;
                case KeyCode.RightWindows when simply:
                    text = "LW";
                    break;
                case KeyCode.Semicolon:
                    text = ";";
                    break;
                case KeyCode.Slash:
                    text = "/";
                    break;
                case KeyCode.Space when simply:
                    text = "???";
                    break;
                case KeyCode.SysReq when !simply:
                    text = "Print Screen";
                    break;
                case KeyCode.SysReq when simply:
                    text = "PS";
                    break;
                case KeyCode.Tilde:
                    text = "~";
                    break;
                case KeyCode.Underscore:
                    text = "_";
                    break;
                case KeyCode.UpArrow:
                    text = "???";
                    break;
                case KeyCode.DownArrow:
                    text = "???";
                    break;
                case KeyCode.LeftArrow:
                    text = "???";
                    break;
                case KeyCode.RightArrow:
                    text = "???";
                    break;
                case KeyCode.LeftControl when !simply:
                    text = "Left Ctrl";
                    break;
                case KeyCode.RightControl when !simply:
                    text = "Right Ctrl";
                    break;
                case KeyCode.LeftControl when simply:
                    text = "LC";
                    break;
                case KeyCode.RightControl when simply:
                    text = "RC";
                    break;
                case KeyCode.LeftAlt when simply:
                    text = "LA";
                    break;
                case KeyCode.RightAlt when simply:
                    text = "RA";
                    break;
                case KeyCode.LeftShift when simply:
                    text = "L???";
                    break;
                case KeyCode.RightShift when simply:
                    text = "R???";
                    break;
                case KeyCode.Backspace when simply:
                    text = "B???";
                    break;
                case KeyCode.Delete when simply:
                    text = "D???";
                    break;
                case KeyCode.PageUp when simply:
                    text = "P???";
                    break;
                case KeyCode.PageDown when simply:
                    text = "P???";
                    break;
                default:
                    text = keyCode.ToString().AddSpacesToSentence();
                    break;
            }

            return text;
        }
        #endregion

        #region To Bar
        /// <summary>
        /// (value = 5, max = 10, length = 10) = "??????????????????????????????"
        /// </summary>
        /// <param name="value"></param>
        /// <param name="max"></param>
        /// <param name="length"></param>
        /// <returns></returns>
        public static string ToBar(this int value, int max, int length, string fill = "???", string half = "???", string empty = "???") => ToBar((double)value, max, length, fill, half, empty);

        /// <summary>
        /// (value = 5.5, max = 10, length = 10) = "??????????????????????????????"
        /// </summary>
        /// <param name="value"></param>
        /// <param name="max"></param>
        /// <param name="length"></param>
        /// <returns></returns>
        public static string ToBar(this float value, float max, int length, string fill = "???", string half = "???", string empty = "???") => ToBar((double)value, max, length, fill, half, empty);

        /// <summary>
        /// (value = 5.5, max = 10, length = 10) = "??????????????????????????????"
        /// </summary>
        /// <param name="value"></param>
        /// <param name="max"></param>
        /// <param name="length"></param>
        /// <returns></returns>
        public static string ToBar(this double value, double max, int length, string fill = "???", string half = "???", string empty = "???")
        {
            if (fill == null)
                fill = "";
            if (half == null)
                half = "";
            if (empty == null)
                empty = "";

            string text = "";

            for (double i = 0.5; i < length + 0.5; i++)
            {
                if (value / max >= i / length)
                    text += fill;
                else
                {
                    if (value / max >= (i - 0.5) / length)
                        text += half;
                    else
                        text += empty;
                }
            }
            return text;
        }
        #endregion

        public static string DataSizeToString(this long byteSize) => ByteTo(byteSize, out string space) + space;

        /// <summary>
        /// ????????? ?????????(?????????) ???????????? ??????????????? (B, KB, MB, GB, TB, PB, EB, ZB, YB)
        /// </summary>
        /// <param name="byteSize">????????? ??????</param>
        /// <param name="digits">????????? ???????????? ????????? ????????? ?????????</param>
        /// <returns>????????? ??????</returns>
        public static string DataSizeToString(this long byteSize, int digits) => ByteTo(byteSize, out string space).Round(digits) + space;

        /// <summary>
        /// ????????? ?????????(?????????) ???????????? ??????????????? (B, KB, MB, GB, TB, PB, EB, ZB, YB)
        /// </summary>
        /// <param name="byteSize">????????? ??????</param>
        public static double ByteTo(long byteSize, out string space)
        {
            int loopCount = 0;
            double size = byteSize;

            while (size > 1024.0)
            {
                size /= 1024.0;
                loopCount++;
            }

            if (loopCount == 0)
                space = "B";
            else if (loopCount == 1)
                space = "KB";
            else if (loopCount == 2)
                space = "MB";
            else if (loopCount == 3)
                space = "GB";
            else if (loopCount == 4)
                space = "TB";
            else if (loopCount == 5)
                space = "PB";
            else if (loopCount == 6)
                space = "EB";
            else if (loopCount == 7)
                space = "ZB";
            else
                space = "YB";

            return size;
        }

        public static double ByteTo(long byteSize, DataSizeType dataSizeType, out string space)
        {
            double size = byteSize / Math.Pow(1024, (int)dataSizeType);
            space = dataSizeType.ToString().ToUpper();

            return size;
        }

        public enum DataSizeType
        {
            b,
            kb,
            mb,
            gb,
            tb,
            pb,
            eb,
            zb,
            yb
        }
    }

    public static class PathTool
    {
        /// <summary>
        /// (paths = ("asdf", "asdf")) = "asdf/asdf" (Path.Combine is "asdf\asdf")
        /// </summary>
        /// <param name="path1">????????? ??????</param>
        /// <param name="path2">????????? ??????</param>
        /// <returns></returns>
        public static string Combine(string path1, string path2) => Path.Combine(path1, path2).Replace("\\", "/");
        /// <summary>
        /// (paths = ("asdf", "asdf")) = "asdf/asdf" (Path.Combine is "asdf\asdf")
        /// </summary>
        /// <param name="path1">????????? ??????</param>
        /// <param name="path2">????????? ??????</param>
        /// <param name="path3">????????? ??????</param>
        /// <returns></returns>
        public static string Combine(string path1, string path2, string path3) => Path.Combine(path1, path2, path3).Replace("\\", "/");
        /// <summary>
        /// (paths = ("asdf", "asdf")) = "asdf/asdf" (Path.Combine is "asdf\asdf")
        /// </summary>
        /// <param name="path1">????????? ??????</param>
        /// <param name="path2">????????? ??????</param>
        /// <param name="path3">????????? ??????</param>
        /// <param name="path4">????????? ??????</param>
        /// <returns></returns>
        public static string Combine(string path1, string path2, string path3, string path4) => Path.Combine(path1, path2, path3, path4).Replace("\\", "/");
        /// <summary>
        /// (paths = ("asdf", "asdf")) = "asdf/asdf" (Path.Combine is "asdf\asdf")
        /// </summary>
        /// <param name="paths">?????????</param>
        /// <returns></returns>
        public static string Combine(params string[] paths) => Path.Combine(paths).Replace("\\", "/");

        public static string RemoveInvalidPathChars(string filename) => string.Concat(filename.Split(Path.GetInvalidPathChars()));
        public static string ReplaceInvalidPathChars(string filename) => string.Join("_", filename.Split(Path.GetInvalidPathChars()));

        public static string RemoveInvalidFileNameChars(string filename) => string.Concat(filename.Split(Path.GetInvalidFileNameChars()));
        public static string ReplaceInvalidFileNameChars(string filename) => string.Join("_", filename.Split(Path.GetInvalidFileNameChars()));

        public static string GetPathWithExtension(string path) => path.Remove(path.Length - Path.GetExtension(path).Length);
    }

    public static class DirectoryTool
    {
        public static void Copy(string sourceFolder, string destFolder)
        {
            if (!Directory.Exists(destFolder))
                Directory.CreateDirectory(destFolder);

            string[] files = Directory.GetFiles(sourceFolder);
            string[] folders = Directory.GetDirectories(sourceFolder);

            for (int i = 0; i < files.Length; i++)
            {
                string file = files[i];
                string name = Path.GetFileName(file);
                string dest = Path.Combine(destFolder, name);
                File.Copy(file, dest);
            }

            for (int i = 0; i < folders.Length; i++)
            {
                string folder = folders[i];
                string name = Path.GetFileName(folder);
                string dest = Path.Combine(destFolder, name);
                Copy(folder, dest);
            }
        }

        public static string[] GetFiles(string path, params string[] searchPatterns) => GetFiles(path, searchPatterns, SearchOption.TopDirectoryOnly);
        public static string[] GetFiles(string path, string[] searchPatterns, SearchOption searchOption)
        {
            List<string> paths = new List<string>();
            for (int i = 0; i < searchPatterns.Length; i++)
            {
                string searchPattern = searchPatterns[i];
                paths.AddRange(Directory.GetFiles(path, searchPattern, searchOption));
            }

            return paths.ToArray();
        }
    }

    public static class TimeTool
    {
        #region To Time
        /// <summary>
        /// (second = 70) = "1:10"
        /// </summary>
        /// <param name="second">???</param>
        /// <param name="minuteAlwayShow">??? ?????? ?????? ??????</param>
        /// <param name="hourAlwayShow">??????, ??? ?????? ?????? ??????</param>
        /// <param name="dayAlwayShow">??????, ??????, ??? ?????? ?????? ??????</param>
        /// <returns></returns>
        public static string ToTime(this int second, bool minuteAlwayShow = false, bool hourAlwayShow = false, bool dayAlwayShow = false)
        {
            try
            {
                int secondAbs = second.Abs();
                if (second <= TimeSpan.MinValue.TotalSeconds)
                    return TimeSpan.MinValue.ToString(@"d\:hh\:mm\:ss");
                else if (second > TimeSpan.MaxValue.TotalSeconds)
                    return TimeSpan.MaxValue.ToString(@"d\:h\:mm\:ss");
                else if (secondAbs >= 86400 || dayAlwayShow)
                    return TimeSpan.FromSeconds(second).ToString(@"d\:hh\:mm\:ss");
                else if (secondAbs >= 3600 || hourAlwayShow)
                    return TimeSpan.FromSeconds(second).ToString(@"h\:mm\:ss");
                else if (secondAbs >= 60 || minuteAlwayShow)
                    return TimeSpan.FromSeconds(second).ToString(@"m\:ss");
                else
                    return TimeSpan.FromSeconds(second).ToString(@"s");
            }
            catch (Exception e)
            {
                Debug.LogException(e);
                return "--:--";
            }
        }

        /// <summary>
        /// (second = 70.1f) = "1:10.1"
        /// </summary>
        /// <param name="second">???</param>
        /// <param name="decimalShow">?????? ??????</param>
        /// <param name="minuteAlwayShow">??? ?????? ?????? ??????</param>
        /// <param name="hourAlwayShow">??????, ??? ?????? ?????? ??????</param>
        /// <param name="dayAlwayShow">??????, ??????, ??? ?????? ?????? ??????</param>
        /// <returns></returns>
        public static string ToTime(this float second, bool decimalShow = true, bool minuteAlwayShow = false, bool hourAlwayShow = false, bool dayAlwayShow = false) => ToTime((double)second, decimalShow, minuteAlwayShow, hourAlwayShow, dayAlwayShow);

        /// <summary>
        /// (second = 70.1) = "1:10.1"
        /// </summary>
        /// <param name="second">???</param>
        /// <param name="decimalShow">?????? ??????</param>
        /// <param name="minuteAlwayShow">??? ?????? ?????? ??????</param>
        /// <param name="hourAlwayShow">??????, ??? ?????? ?????? ??????</param>
        /// <param name="dayAlwayShow">??????, ??????, ??? ?????? ?????? ??????</param>
        /// <returns></returns>
        public static string ToTime(this double second, bool decimalShow = true, bool minuteAlwayShow = false, bool hourAlwayShow = false, bool dayAlwayShow = false)
        {
            try
            {
                double secondAbs = second.Abs();
                if (decimalShow)
                {
                    if (second <= TimeSpan.MinValue.TotalSeconds || secondAbs == float.NegativeInfinity)
                        return TimeSpan.MinValue.ToString(@"d\:hh\:mm\:ss\.ff");
                    else if (second > TimeSpan.MaxValue.TotalSeconds || secondAbs == float.PositiveInfinity)
                        return TimeSpan.FromSeconds(TimeSpan.MaxValue.TotalSeconds).ToString(@"d\:hh\:mm\:ss\.ff");
                    else if (secondAbs >= 86400 || dayAlwayShow)
                        return TimeSpan.FromSeconds(second).ToString(@"d\:hh\:mm\:ss\.ff");
                    else if (secondAbs >= 3600 || hourAlwayShow)
                        return TimeSpan.FromSeconds(second).ToString(@"h\:mm\:ss\.ff");
                    else if (secondAbs >= 60 || minuteAlwayShow)
                        return TimeSpan.FromSeconds(second).ToString(@"m\:ss\.ff");
                    else
                        return TimeSpan.FromSeconds(second).ToString(@"s\.ff");
                }
                else
                {
                    if (second <= TimeSpan.MinValue.TotalSeconds || secondAbs == float.NegativeInfinity)
                        return TimeSpan.MinValue.ToString(@"d\:hh\:mm\:ss");
                    else if (second > TimeSpan.MaxValue.TotalSeconds || secondAbs == float.PositiveInfinity)
                        return TimeSpan.FromSeconds(TimeSpan.MaxValue.TotalSeconds).ToString(@"d\:h\:mm\:ss");
                    else if (secondAbs >= 86400 || dayAlwayShow)
                        return TimeSpan.FromSeconds(second).ToString(@"d\:hh\:mm\:ss");
                    else if (secondAbs >= 3600 || hourAlwayShow)
                        return TimeSpan.FromSeconds(second).ToString(@"h\:mm\:ss");
                    else if (secondAbs >= 60 || minuteAlwayShow)
                        return TimeSpan.FromSeconds(second).ToString(@"m\:ss");
                    else
                        return TimeSpan.FromSeconds(second).ToString(@"s");
                }
            }
            catch (Exception e) 
            {
                Debug.LogException(e);
                return "--:--";
            }
        }
        #endregion

        public static DateTime ToLunarDate(this DateTime dateTime)
        {
            KoreanLunisolarCalendar klc = new KoreanLunisolarCalendar();

            int year = klc.GetYear(dateTime);
            int month = klc.GetMonth(dateTime);
            int day = klc.GetDayOfMonth(dateTime);

            //1?????? 12???????????? ????????? ??????..
            if (klc.GetMonthsInYear(year) > 12)
            {
                //????????? ????????? ?????????????
                int leapMonth = klc.GetLeapMonth(year);

                //?????? ???????????? ????????? ?????? -1??? ??? ??? ???8???->9 ????????????
                if (month >= leapMonth)
                    month--;
            }

            return new DateTime(year, month, day);
        }

        public static DateTime ToSolarDate(this DateTime dateTime, bool isLeapMonth = false)
        {
            KoreanLunisolarCalendar klc = new KoreanLunisolarCalendar();

            int year = dateTime.Year;
            int month = dateTime.Month;
            int day = dateTime.Day;

            if (klc.GetMonthsInYear(year) > 12)
            {
                int leapMonth = klc.GetLeapMonth(year);

                if (month > leapMonth - 1)
                    month++;
                else if (month == leapMonth - 1 && isLeapMonth)
                    month++;
            }

            return klc.ToDateTime(year, month, day, 0, 0, 0, 0);
        }
    }

    public static class ComponentTool
    {
        public static T GetComponentFieldSave<T>(this Component component, T fieldToSave, GetComponentMode mode = GetComponentMode.addIfNull) where T : Component
        {
            if (fieldToSave == null || fieldToSave.gameObject != component.gameObject)
            {
                fieldToSave = component.GetComponent<T>();
                if (fieldToSave == null)
                {
                    if (mode == GetComponentMode.addIfNull)
                        return component.gameObject.AddComponent<T>();
                    else if (mode == GetComponentMode.destroyIfNull)
                    {
                        UnityEngine.Object.DestroyImmediate(component);
                        return null;
                    }
                }
            }

            return fieldToSave;
        }

        public static T[] GetComponentsInChildrenFieldSave<T>(this Component component, T[] fieldToSave, bool includeInactive = false, GetComponentsInChildrenMode mode = GetComponentsInChildrenMode.addZeroLengthIfNull)
        {
            if (fieldToSave == null)
            {
                fieldToSave = component.GetComponentsInChildren<T>(includeInactive);
                if (fieldToSave == null)
                {
                    if (mode == GetComponentsInChildrenMode.addZeroLengthIfNull)
                        return new T[0];
                    else if (mode == GetComponentsInChildrenMode.destroyIfNull)
                    {
                        UnityEngine.Object.DestroyImmediate(component);
                        return null;
                    }
                }
            }

            return fieldToSave;
        }

        public enum GetComponentMode
        {
            none,
            addIfNull,
            destroyIfNull
        }

        public enum GetComponentsInChildrenMode
        {
            none,
            addZeroLengthIfNull,
            destroyIfNull
        }
    }
}