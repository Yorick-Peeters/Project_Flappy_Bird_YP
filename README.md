# Project_Flappy_Bird_YP

This is a small Flappy Bird-like Unity project implemented in C# for the Advanced C# Programming course.

How to build & run
 - Open `Project_Flappy_Bird_YP.sln` or open the folder in Unity (Unity Editor 2021.3+ recommended).
 - Make sure the Unity UI package is available (project already contains UI usage).
 - Open the `Assets/Scenes` scene you want to run, or open the `SampleScene` if provided.
 - Enter Play mode in the Unity Editor to run the game.

Running tests
 - Open Unity Test Runner (Window -> General -> Test Runner).
 - Run EditMode tests (the test added: `GameManagerTests`) to check the score increment behavior.

Implemented patterns and notes
 - Observer (events): `Player` now exposes `OnDeath` and `OnScore` events; `GameManager` subscribes and unsubscribes. This decouples player and manager.
 - Singleton: `GameManager` now exposes a simple `public static GameManager Instance` for global access. This is a common Unity pattern but be mindful of lifecycle and testing.
 - Object Pooling: added `PipePool` (plus `PooledObject`) and updated `PipeSpawner` and `Pipes` to use the pool when available. This reduces instantiate/destroy churn and GC pressure when many pipes are spawned.

