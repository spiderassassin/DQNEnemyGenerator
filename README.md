# DQNEnemyGenerator

### How to run in Unity Editor (torch device optional):

```
mlagents-learn PacManUnity/config/bc.yaml --force --run-id=ExampleRunBC --torch-device=cpu
```

### How to run with build (torch device optional):

```
mlagents-learn PacManUnity/config/hp.yaml --force --run-id=ExampleRun --env='./PacManUnity/Build/Game AI.exe' --quality-level=0 --width=512 --height=512 --torch-device=cpu
```

### How to create a demonstration file (use SceneBHC)

Just press play in the scene, output saved to PacManUnity/Demonstrations.
