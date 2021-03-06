using Cysharp.Threading.Tasks;
using System;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

namespace SCKRM.Threads
{
    public static class ThreadManager
    {
        public static List<ThreadMetaData> runningThreads { get; } = new List<ThreadMetaData>();

        /// <summary>
        /// { get; } = Thread.CurrentThread.ManagedThreadId
        /// </summary>
        public static int mainThreadId { get; } = Thread.CurrentThread.ManagedThreadId;

        /// <summary>
        /// { get { mainThreadId == Thread.CurrentThread.ManagedThreadId; } }
        /// </summary>
        public static bool isMainThread => mainThreadId == Thread.CurrentThread.ManagedThreadId;


        public static event Action threadAdd = () => { };
        public static event Action threadChange = () => { };
        public static event Action threadRemove = () => { };

        public static void ThreadAddEventInvoke() => threadAdd();
        public static void ThreadRemoveEventInvoke() => threadRemove();
        public static void ThreadChangeEventInvoke() => threadChange();



        public static void AllThreadRemove()
        {
            if (!isMainThread)
                throw new NotMainThreadMethodException(nameof(AllThreadRemove));

            for (int i = 0; i < runningThreads.Count; i++)
                runningThreads[i]?.Remove(true);
        }

        public static async UniTaskVoid ThreadAutoRemove()
        {
            if (!isMainThread)
                throw new NotMainThreadMethodException();

            while (true)
            {
                if (!Kernel.isPlaying)
                    return;

                for (int i = 0; i < runningThreads.Count; i++)
                {
                    ThreadMetaData runningThread = runningThreads[i];
                    if (runningThread != null && !runningThread.autoRemoveDisable && (runningThread.thread == null || !runningThread.thread.IsAlive))
                        runningThread.Remove(true);
                }

                if (await UniTask.Delay(100, cancellationToken: AsyncTaskManager.cancelToken).SuppressCancellationThrow())
                    return;
            }
        }

#pragma warning disable CS0618 // ?????? ?????? ????????? ???????????? ????????????.
        #region Thread Create Method
        public static ThreadMetaData Create(Action method, string name = "", string info = "", bool loop = false, bool autoRemoveDisable = false, bool cantCancel = true)
        {
            if (!isMainThread)
                throw new NotMainThreadMethodException(nameof(Create));
            if (!Kernel.isPlaying)
                throw new NotPlayModeThreadCreateException();

            ThreadMetaData threadMetaData = new ThreadMetaData(name, info, loop, autoRemoveDisable, cantCancel);
            threadMetaData.thread = new Thread(() => method());
            threadMetaData.thread.Start();

            ThreadAddEventInvoke();
            ThreadChangeEventInvoke();
            

            return threadMetaData;
        }
        public static ThreadMetaData Create(Action<ThreadMetaData> method, string name = "", string info = "", bool loop = false, bool autoRemoveDisable = false, bool cantCancel = true)
        {
            if (!isMainThread)
                throw new NotMainThreadMethodException(nameof(Create));
            if (!Kernel.isPlaying)
                throw new NotPlayModeThreadCreateException();

            ThreadMetaData threadMetaData = new ThreadMetaData(name, info, loop, autoRemoveDisable, cantCancel);
            threadMetaData.thread = new Thread(() => method(threadMetaData));
            threadMetaData.thread.Start();

            ThreadAddEventInvoke();
            ThreadChangeEventInvoke();
            

            return threadMetaData;
        }
        public static ThreadMetaData Create<T>(Action<T, ThreadMetaData> method, T obj, string name = "", string info = "", bool loop = false, bool autoRemoveDisable = false, bool cantCancel = true)
        {
            if (!isMainThread)
                throw new NotMainThreadMethodException(nameof(Create));
            if (!Kernel.isPlaying)
                throw new NotPlayModeThreadCreateException();

            ThreadMetaData threadMetaData = new ThreadMetaData(name, info, loop, autoRemoveDisable, cantCancel);
            threadMetaData.thread = new Thread(() => method(obj, threadMetaData));
            threadMetaData.thread.Start();

            ThreadAddEventInvoke();
            ThreadChangeEventInvoke();
            

            return threadMetaData;
        }
        public static ThreadMetaData Create<T1, T2>(Action<T1, T2, ThreadMetaData> method, T1 arg1, T2 arg2, string name = "", string info = "", bool loop = false, bool autoRemoveDisable = false, bool cantCancel = true)
        {
            if (!isMainThread)
                throw new NotMainThreadMethodException(nameof(Create));
            if (!Kernel.isPlaying)
                throw new NotPlayModeThreadCreateException();

            ThreadMetaData threadMetaData = new ThreadMetaData(name, info, loop, autoRemoveDisable, cantCancel);
            threadMetaData.thread = new Thread(() => method(arg1, arg2, threadMetaData));
            threadMetaData.thread.Start();

            ThreadAddEventInvoke();
            ThreadChangeEventInvoke();
            

            return threadMetaData;
        }
        public static ThreadMetaData Create<T1, T2, T3>(Action<T1, T2, T3, ThreadMetaData> method, T1 arg1, T2 arg2, T3 arg3, string name = "", string info = "", bool loop = false, bool autoRemoveDisable = false, bool cantCancel = true)
        {
            if (!isMainThread)
                throw new NotMainThreadMethodException(nameof(Create));
            if (!Kernel.isPlaying)
                throw new NotPlayModeThreadCreateException();

            ThreadMetaData threadMetaData = new ThreadMetaData(name, info, loop, autoRemoveDisable, cantCancel);
            threadMetaData.thread = new Thread(() => method(arg1, arg2, arg3, threadMetaData));
            threadMetaData.thread.Start();

            ThreadAddEventInvoke();
            ThreadChangeEventInvoke();
            

            return threadMetaData;
        }
        public static ThreadMetaData Create<T1, T2, T3, T4>(Action<T1, T2, T3, T4, ThreadMetaData> method, T1 arg1, T2 arg2, T3 arg3, T4 arg4, string name = "", string info = "", bool loop = false, bool autoRemoveDisable = false, bool cantCancel = true)
        {
            if (!isMainThread)
                throw new NotMainThreadMethodException(nameof(Create));
            if (!Kernel.isPlaying)
                throw new NotPlayModeThreadCreateException();

            ThreadMetaData threadMetaData = new ThreadMetaData(name, info, loop, autoRemoveDisable, cantCancel);
            threadMetaData.thread = new Thread(() => method(arg1, arg2, arg3, arg4, threadMetaData));
            threadMetaData.thread.Start();

            ThreadAddEventInvoke();
            ThreadChangeEventInvoke();
            

            return threadMetaData;
        }
        public static ThreadMetaData Create<T1, T2, T3, T4, T5>(Action<T1, T2, T3, T4, T5, ThreadMetaData> method, T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, string name = "", string info = "", bool loop = false, bool autoRemoveDisable = false, bool cantCancel = true)
        {
            if (!isMainThread)
                throw new NotMainThreadMethodException(nameof(Create));
            if (!Kernel.isPlaying)
                throw new NotPlayModeThreadCreateException();

            ThreadMetaData threadMetaData = new ThreadMetaData(name, info, loop, autoRemoveDisable, cantCancel);
            threadMetaData.thread = new Thread(() => method(arg1, arg2, arg3, arg4, arg5, threadMetaData));
            threadMetaData.thread.Start();

            ThreadAddEventInvoke();
            ThreadChangeEventInvoke();
            

            return threadMetaData;
        }
        public static ThreadMetaData Create<T1, T2, T3, T4, T5, T6>(Action<T1, T2, T3, T4, T5, T6, ThreadMetaData> method, T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6, string name = "", string info = "", bool loop = false, bool autoRemoveDisable = false, bool cantCancel = true)
        {
            if (!isMainThread)
                throw new NotMainThreadMethodException(nameof(Create));
            if (!Kernel.isPlaying)
                throw new NotPlayModeThreadCreateException();

            ThreadMetaData threadMetaData = new ThreadMetaData(name, info, loop, autoRemoveDisable, cantCancel);
            threadMetaData.thread = new Thread(() => method(arg1, arg2, arg3, arg4, arg5, arg6, threadMetaData));
            threadMetaData.thread.Start();

            ThreadAddEventInvoke();
            ThreadChangeEventInvoke();
            

            return threadMetaData;
        }
        public static ThreadMetaData Create<T1, T2, T3, T4, T5, T6, T7>(Action<T1, T2, T3, T4, T5, T6, T7, ThreadMetaData> method, T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6, T7 arg7, string name = "", string info = "", bool loop = false, bool autoRemoveDisable = false, bool cantCancel = true)
        {
            if (!isMainThread)
                throw new NotMainThreadMethodException(nameof(Create));

            if (!Kernel.isPlaying)
                throw new NotPlayModeThreadCreateException();

            ThreadMetaData threadMetaData = new ThreadMetaData(name, info, loop, autoRemoveDisable, cantCancel);
            threadMetaData.thread = new Thread(() => method(arg1, arg2, arg3, arg4, arg5, arg6, arg7, threadMetaData));
            threadMetaData.thread.Start();

            ThreadAddEventInvoke();
            ThreadChangeEventInvoke();
            

            return threadMetaData;
        }
        public static ThreadMetaData Create<T1, T2, T3, T4, T5, T6, T7, T8>(Action<T1, T2, T3, T4, T5, T6, T7, T8, ThreadMetaData> method, T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6, T7 arg7, T8 arg8, string name = "", string info = "", bool loop = false, bool autoRemoveDisable = false, bool cantCancel = true)
        {
            if (!isMainThread)
                throw new NotMainThreadMethodException(nameof(Create));
            if (!Kernel.isPlaying)
                throw new NotPlayModeThreadCreateException();

            ThreadMetaData threadMetaData = new ThreadMetaData(name, info, loop, autoRemoveDisable, cantCancel);
            threadMetaData.thread = new Thread(() => method(arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, threadMetaData));
            threadMetaData.thread.Start();

            ThreadAddEventInvoke();
            ThreadChangeEventInvoke();
            

            return threadMetaData;
        }
        public static ThreadMetaData Create<T1, T2, T3, T4, T5, T6, T7, T8, T9>(Action<T1, T2, T3, T4, T5, T6, T7, T8, T9, ThreadMetaData> method, T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6, T7 arg7, T8 arg8, T9 arg9, string name = "", string info = "", bool loop = false, bool autoRemoveDisable = false, bool cantCancel = true)
        {
            if (!isMainThread)
                throw new NotMainThreadMethodException(nameof(Create));
            if (!Kernel.isPlaying)
                throw new NotPlayModeThreadCreateException();

            ThreadMetaData threadMetaData = new ThreadMetaData(name, info, loop, autoRemoveDisable, cantCancel);
            threadMetaData.thread = new Thread(() => method(arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, threadMetaData));
            threadMetaData.thread.Start();

            ThreadAddEventInvoke();
            ThreadChangeEventInvoke();
            

            return threadMetaData;
        }
        public static ThreadMetaData Create<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10>(Action<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, ThreadMetaData> method, T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6, T7 arg7, T8 arg8, T9 arg9, T10 arg10, string name = "", string info = "", bool loop = false, bool autoRemoveDisable = false, bool cantCancel = true)
        {
            if (!isMainThread)
                throw new NotMainThreadMethodException(nameof(Create));
            if (!Kernel.isPlaying)
                throw new NotPlayModeThreadCreateException();

            ThreadMetaData threadMetaData = new ThreadMetaData(name, info, loop, autoRemoveDisable, cantCancel);
            threadMetaData.thread = new Thread(() => method(arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, threadMetaData));
            threadMetaData.thread.Start();

            ThreadAddEventInvoke();
            ThreadChangeEventInvoke();
            

            return threadMetaData;
        }
        public static ThreadMetaData Create<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11>(Action<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, ThreadMetaData> method, T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6, T7 arg7, T8 arg8, T9 arg9, T10 arg10, T11 arg11, string name = "", string info = "", bool loop = false, bool autoRemoveDisable = false, bool cantCancel = true)
        {
            if (!isMainThread)
                throw new NotMainThreadMethodException(nameof(Create));
            if (!Kernel.isPlaying)
                throw new NotPlayModeThreadCreateException();

            ThreadMetaData threadMetaData = new ThreadMetaData(name, info, loop, autoRemoveDisable, cantCancel);
            threadMetaData.thread = new Thread(() => method(arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11, threadMetaData));
            threadMetaData.thread.Start();

            ThreadAddEventInvoke();
            ThreadChangeEventInvoke();
            

            return threadMetaData;
        }
        public static ThreadMetaData Create<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12>(Action<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, ThreadMetaData> method, T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6, T7 arg7, T8 arg8, T9 arg9, T10 arg10, T11 arg11, T12 arg12, string name = "", string info = "", bool loop = false, bool autoRemoveDisable = false, bool cantCancel = true)
        {
            if (!isMainThread)
                throw new NotMainThreadMethodException(nameof(Create));
            if (!Kernel.isPlaying)
                throw new NotPlayModeThreadCreateException();

            ThreadMetaData threadMetaData = new ThreadMetaData(name, info, loop, autoRemoveDisable, cantCancel);
            threadMetaData.thread = new Thread(() => method(arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11, arg12, threadMetaData));
            threadMetaData.thread.Start();

            ThreadAddEventInvoke();
            ThreadChangeEventInvoke();
            

            return threadMetaData;
        }
        public static ThreadMetaData Create<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13>(Action<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, ThreadMetaData> method, T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6, T7 arg7, T8 arg8, T9 arg9, T10 arg10, T11 arg11, T12 arg12, T13 arg13, string name = "", string info = "", bool loop = false, bool autoRemoveDisable = false, bool cantCancel = true)
        {
            if (!isMainThread)
                throw new NotMainThreadMethodException(nameof(Create));
            if (!Kernel.isPlaying)
                throw new NotPlayModeThreadCreateException();

            ThreadMetaData threadMetaData = new ThreadMetaData(name, info, loop, autoRemoveDisable, cantCancel);
            threadMetaData.thread = new Thread(() => method(arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11, arg12, arg13, threadMetaData));
            threadMetaData.thread.Start();

            ThreadAddEventInvoke();
            ThreadChangeEventInvoke();
            

            return threadMetaData;
        }
        public static ThreadMetaData Create<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14>(Action<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, ThreadMetaData> method, T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6, T7 arg7, T8 arg8, T9 arg9, T10 arg10, T11 arg11, T12 arg12, T13 arg13, T14 arg14, string name = "", string info = "", bool loop = false, bool autoRemoveDisable = false, bool cantCancel = true)
        {
            if (!isMainThread)
                throw new NotMainThreadMethodException(nameof(Create));
            if (!Kernel.isPlaying)
                throw new NotPlayModeThreadCreateException();

            ThreadMetaData threadMetaData = new ThreadMetaData(name, info, loop, autoRemoveDisable, cantCancel);
            threadMetaData.thread = new Thread(() => method(arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11, arg12, arg13, arg14, threadMetaData));
            threadMetaData.thread.Start();

            ThreadAddEventInvoke();
            ThreadChangeEventInvoke();
            

            return threadMetaData;
        }
        public static ThreadMetaData Create<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15>(Action<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, ThreadMetaData> method, T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6, T7 arg7, T8 arg8, T9 arg9, T10 arg10, T11 arg11, T12 arg12, T13 arg13, T14 arg14, T15 arg15, string name = "", string info = "", bool loop = false, bool autoRemoveDisable = false, bool cantCancel = true)
        {
            if (!isMainThread)
                throw new NotMainThreadMethodException(nameof(Create));
            if (!Kernel.isPlaying)
                throw new NotPlayModeThreadCreateException();

            ThreadMetaData threadMetaData = new ThreadMetaData(name, info, loop, autoRemoveDisable, cantCancel);
            threadMetaData.thread = new Thread(() => method(arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11, arg12, arg13, arg14, arg15, threadMetaData));
            threadMetaData.thread.Start();

            ThreadAddEventInvoke();
            ThreadChangeEventInvoke();
            

            return threadMetaData;
        }
        #endregion
#pragma warning restore CS0618 // ?????? ?????? ????????? ???????????? ????????????.
    }

    public sealed class ThreadMetaData : AsyncTask
    {
        int nameLock = 0;
        string _name = "";
        /// <summary>
        /// Thread-Safe
        /// </summary>
        public override string name
        {
            get
            {
                while (Interlocked.CompareExchange(ref nameLock, 1, 0) != 0)
                    Thread.Sleep(1);

                string value = _name;

                Interlocked.Decrement(ref nameLock);
                return value;
            }
            set
            {
                while (Interlocked.CompareExchange(ref nameLock, 1, 0) != 0)
                    Thread.Sleep(1);

                _name = value;
                Interlocked.Decrement(ref nameLock);
            }
        }

        int infoLock = 0;
        string _info = "";
        /// <summary>
        /// Thread-Safe
        /// </summary>
        public override string info
        {
            get
            {
                while (Interlocked.CompareExchange(ref infoLock, 1, 0) != 0)
                    Thread.Sleep(1);

                string value = _info;

                Interlocked.Decrement(ref infoLock);
                return value;
            }
            set
            {
                while (Interlocked.CompareExchange(ref infoLock, 1, 0) != 0)
                    Thread.Sleep(1);

                _info = value;
                Interlocked.Decrement(ref infoLock);
            }
        }

        int loopLock = 0;
        bool _loop = false;
        /// <summary>
        /// Thread-Safe
        /// </summary>
        public override bool loop
        {
            get
            {
                while (Interlocked.CompareExchange(ref loopLock, 1, 0) != 0)
                    Thread.Sleep(1);

                bool value = _loop;

                Interlocked.Decrement(ref loopLock);
                return value;
            }
            set
            {
                while (Interlocked.CompareExchange(ref loopLock, 1, 0) != 0)
                    Thread.Sleep(1);

                _loop = value;
                Interlocked.Decrement(ref loopLock);
            }
        }

        int cantCancelLock = 0;
        bool _cantCancel = false;
        /// <summary>
        /// Thread-Safe
        /// </summary>
        public override bool cantCancel
        {
            get
            {
                while (Interlocked.CompareExchange(ref cantCancelLock, 1, 0) != 0)
                    Thread.Sleep(1);

                bool value = _cantCancel;

                Interlocked.Decrement(ref cantCancelLock);
                return value;
            }
            set
            {
                while (Interlocked.CompareExchange(ref cantCancelLock, 1, 0) != 0)
                    Thread.Sleep(1);

                _cantCancel = value;
                Interlocked.Decrement(ref cantCancelLock);
            }
        }



        int progressLock = 0;
        float _progress = 0;
        /// <summary>
        /// Thread-Safe
        /// </summary>
        public override float progress
        {
            get
            {
                while (Interlocked.CompareExchange(ref progressLock, 1, 0) != 0)
                {
                    Debug.Log("asdf");
                    Thread.Sleep(1);
                }

                float value = _progress;

                Interlocked.Decrement(ref progressLock);
                return value;
            }
            set
            {
                while (Interlocked.CompareExchange(ref progressLock, 1, 0) != 0)
                {
                    Debug.Log("asdf");
                    Thread.Sleep(1);
                }

                _progress = value;
                Interlocked.Decrement(ref progressLock);
            }
        }

        int maxProgressLock = 0;
        float _maxProgress = 0;
        /// <summary>
        /// Thread-Safe
        /// </summary>
        public override float maxProgress
        {
            get
            {
                while (Interlocked.CompareExchange(ref maxProgressLock, 1, 0) != 0)
                    Thread.Sleep(1);

                float value = _maxProgress;

                Interlocked.Decrement(ref maxProgressLock);
                return value;
            }
            set
            {
                while (Interlocked.CompareExchange(ref maxProgressLock, 1, 0) != 0)
                    Thread.Sleep(1);

                _maxProgress = value;
                Interlocked.Decrement(ref maxProgressLock);
            }
        }



        int isCanceledLock = 0;
        bool _isCanceled = false;
        /// <summary>
        /// Thread-Safe
        /// </summary>
        public override bool isCanceled
        {
            get
            {
                while (Interlocked.CompareExchange(ref isCanceledLock, 1, 0) != 0)
                    Thread.Sleep(1);

                bool value = _isCanceled;

                Interlocked.Decrement(ref isCanceledLock);
                return value;
            }
            protected set
            {
                while (Interlocked.CompareExchange(ref isCanceledLock, 1, 0) != 0)
                    Thread.Sleep(1);

                _isCanceled = value;

                Interlocked.Decrement(ref isCanceledLock);
            }
        }



        public ThreadMetaData(string name = "", string info = "", bool loop = false, bool autoRemoveDisable = false, bool cantCancel = true) : base(name, info, loop, cantCancel)
        {
            this.autoRemoveDisable = autoRemoveDisable;
            ThreadManager.runningThreads.Add(this);
        }

        public Thread thread { get; [Obsolete("It is managed by the ThreadManager class. Please do not touch it.", false)] internal set; } = null;
        public bool autoRemoveDisable { get; set; } = false;

        /// <summary>
        /// ??? ????????? ?????? ?????????????????? ???????????? ????????????
        /// This function can only be executed on the main thread
        /// </summary>
        /// <exception cref="NotMainThreadMethodException"></exception>
        public override bool Remove(bool force = false)
        {
            if (!ThreadManager.isMainThread)
                throw new NotMainThreadMethodException(nameof(Remove));

            if (base.Remove(force))
            {
                ThreadManager.runningThreads.Remove(this);

                ThreadManager.ThreadChangeEventInvoke();
                ThreadManager.ThreadRemoveEventInvoke();

                if (thread != null)
                {
                    Thread _thread = thread;
#pragma warning disable CS0618 // ?????? ?????? ????????? ???????????? ????????????.
                    thread = null;
#pragma warning restore CS0618 // ?????? ?????? ????????? ???????????? ????????????.
                    _thread.Join();
                }

                return true;
            }

            return false;
        }
    }

    public class NotMainThreadMethodException : Exception
    {
        /// <summary>
        /// This function must run on the main thread
        /// ??? ????????? ?????? ??????????????? ?????? ?????? ?????????
        /// </summary>
        public NotMainThreadMethodException() : base("This function must run on the main thread\n??? ????????? ?????? ??????????????? ?????? ?????? ?????????") { }

        /// <summary>
        /// {method} function must be executed on the main thread
        /// {method} ????????? ?????? ??????????????? ??????????????? ?????????
        /// </summary>
        public NotMainThreadMethodException(string method) : base($"{method} function must be executed on the main thread\n{method} ????????? ?????? ??????????????? ??????????????? ?????????") { }
    }

    public class MainThreadMethodException : Exception
    {
        /// <summary>
        /// This function cannot be executed on the main thread
        /// ??? ????????? ?????? ??????????????? ?????? ??? ??? ????????????
        /// </summary>
        public MainThreadMethodException() : base("This function cannot be executed on the main thread\n??? ????????? ????????????????????? ?????? ??? ??? ????????????") { }

        /// <summary>
        /// {method} function cannot be executed on the main thread
        /// {method} ????????? ?????? ??????????????? ?????? ??? ??? ????????????
        /// </summary>
        public MainThreadMethodException(string method) : base($"{method} function cannot be executed on the main thread\n{method} ????????? ????????????????????? ?????? ??? ??? ????????????") { }
    }

    public class NotPlayModeThreadCreateException : Exception
    {
        /// <summary>
        /// It is forbidden to spawn threads when not in play mode. Please create your own thread
        /// ????????? ????????? ????????? ???????????? ??????????????? ????????????????????????. ?????? ???????????? ??????????????????
        /// </summary>
        public NotPlayModeThreadCreateException() : base("It is forbidden to spawn threads when not in play mode. Please create your own thread\n????????? ????????? ????????? ???????????? ??????????????? ????????????????????????. ?????? ???????????? ??????????????????") { }
    }
}