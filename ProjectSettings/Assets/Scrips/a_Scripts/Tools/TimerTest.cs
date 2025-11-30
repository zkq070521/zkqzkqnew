using System;
using RPG.Timer;
using UnityEngine;

namespace a_Scripts.Tools
{
    public class TimerTest : MonoBehaviour
    {
        // 1. 初始化你的TimerManager（普通类，直接new，不用AddComponent！）
        private TimerManager _timer;

        // 2. 计时器唯一标识（字符串变量化，避免拼写错误）
        // 时间计时器：测试5秒倒计时
        private string timeTimerKey = "Test_Time_Timer";
        // 帧计时器：测试120帧（约2秒，60帧/秒）
        private string frameTimerKey = "Test_Frame_Timer";

        // 3. 测试参数
        private float testTimeDuration = 5f; // 时间计时器持续时间（5秒）
        private int testFrameCount = 120;    // 帧计时器总帧数（120帧）

        private void Awake()
        {
            // 关键修正：你的TimerManager是普通C#类，不能用AddComponent（只对MonoBehaviour有效）
            // 直接new即可创建实例，不用挂在任何物体上
            _timer = new TimerManager();
        }

        private void Start()
        {
           // Debug.Log("=== 计时器测试开始 ===");
            // 启动两个测试计时器（游戏一开始就启动）
            StartTestTimers();
        }

        private void Update()
        {
            // 每帧执行测试逻辑（实时检测计时器状态）
            RunTimerTests();
        }

        /// <summary>
        /// 启动所有测试计时器
        /// </summary>
        private void StartTestTimers()
        {
            // 测试1：启动【基于时间的计时器】（5秒）
            // 对应你的TimerManager.Start(string key, float duration)方法
            _timer.Start(timeTimerKey, testTimeDuration);
            //Debug.Log($"时间计时器[{timeTimerKey}]已启动，持续{testTimeDuration}秒");

            // 测试2：启动【基于帧的计时器】（120帧）
            // 对应你的TimerManager.Start(string key, int maxFrames)方法
            _timer.Start(frameTimerKey, testFrameCount);
            //Debug.Log($"帧计时器[{frameTimerKey}]已启动，持续{testFrameCount}帧（约{testFrameCount / 60f:F1}秒）");
        }

        /// <summary>
        /// 运行所有计时器测试逻辑（演示每个API的用法）
        /// </summary>
        private void RunTimerTests()
        {
            // ------------------------------ 测试【基于时间的计时器】------------------------------
            if (_timer.Exists(timeTimerKey)) // 先判断计时器是否存在（你的Exists方法）
            {
                // 1. 获取已流逝时间（你的GetElapsed方法）
                float elapsedTime = _timer.GetElapsed(timeTimerKey);
                // 2. 每0.5秒打印一次已流逝时间（避免日志刷屏）
                if (elapsedTime % 0.5f < Time.deltaTime)
                {
                    //Debug.Log($"时间计时器[{timeTimerKey}] - 已流逝：{elapsedTime:F2}秒");
                }

                // 3. 检测是否在1~3秒范围内（你的IsElapsedInRange方法）
                if (_timer.IsElapsedInRange(timeTimerKey, 1f, 3f))
                {
                    // 只打印一次该状态（用帧判断避免重复打印）
                    if (Time.frameCount % 10 == 0)
                    {
                        //Debug.Log($"时间计时器[{timeTimerKey}] - 处于1~3秒区间内");
                    }
                }

                // 4. 检测计时器是否结束（你的IsFinished方法）
                if (_timer.IsFinished(timeTimerKey))
                {
                   // Debug.Log($"时间计时器[{timeTimerKey}] - 5秒倒计时结束！");

                    // 测试重启计时器（你的Restart方法，沿用原有5秒 duration）
                    _timer.Restart(timeTimerKey);
                    //Debug.Log($"时间计时器[{timeTimerKey}]已重启，重新开始5秒倒计时");
                }
            }

            // ------------------------------ 测试【基于帧的计时器】------------------------------
            if (_timer.Exists(frameTimerKey))
            {
                // 1. 获取已流逝帧数（你的GetElapsedFrames方法）
                int elapsedFrames = _timer.GetElapsedFrames(frameTimerKey);
                // 2. 获取已流逝时间（帧转秒，你的GetElapsedTimeFromFrames方法）
                float frameElapsedTime = _timer.GetElapsedTimeFromFrames(frameTimerKey);
                // 3. 获取总持续时间（帧转秒，你的GetDurationTimeFromFrames方法）
                float frameTotalTime = _timer.GetDurationTimeFromFrames(frameTimerKey);

                // 每10帧打印一次帧计时器状态
                if (Time.frameCount % 10 == 0)
                {
                    //Debug.Log($"帧计时器[{frameTimerKey}] - 已流逝：{elapsedFrames}帧 / 总{testFrameCount}帧 " +
                            // $"(已用时间：{frameElapsedTime:F2}秒 / 总时间：{frameTotalTime:F2}秒)");
                }

                // 4. 检测帧计时器是否结束（你的IsFrameFinished方法）
                if (_timer.IsFrameFinished(frameTimerKey))
                {
                   // Debug.Log($" 帧计时器[{frameTimerKey}] - 120帧计数结束！");

                    // 测试重启帧计时器（你的RestartFrame方法，沿用原有120帧）
                    _timer.RestartFrame(frameTimerKey);
                   // Debug.Log($"帧计时器[{frameTimerKey}]已重启，重新开始120帧计数");
                }
            }

            // ------------------------------ 测试【清理已结束计时器】------------------------------
            // 每10秒清理一次（实际项目中可定期调用，这里演示用法）
            if (Time.time % 10f < Time.deltaTime)
            {
                //Debug.Log("\n 开始清理已结束的计时器...");
                _timer.CleanupFinished(); // 你的CleanupFinished方法
                //Debug.Log("清理完成！\n");
            }
        }

        /// <summary>
        /// 测试删除计时器（可绑定按键触发，比如按X删除）
        /// </summary>
        private void LateUpdate()
        {
            // 按X键删除所有测试计时器（演示你的Remove方法）
            if (Input.GetKeyDown(KeyCode.X))
            {
                _timer.Remove(timeTimerKey);
                _timer.Remove(frameTimerKey);
                //Debug.Log($" 已删除所有测试计时器");
            }

            // 按R键重新启动所有计时器
            if (Input.GetKeyDown(KeyCode.R))
            {
                StartTestTimers();
            }
        }
    }
}