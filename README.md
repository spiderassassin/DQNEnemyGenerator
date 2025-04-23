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


### Running evaluations

Run 1000 evaluations, then run 5/10/15 finetuning steps, then run 1000 more evaluations.

First evaluation step:

```
mlagents-learn PacManUnity/config/ft.yaml --force --run-id=EvaluationRun --torch-device=cpu --resume --inference
mlagents-learn PacManUnity/config/ft.yaml --force --run-id=EvaluationRun --env='./PacManUnity/EvaluationBuilds/Game_1000_Steps.exe' --quality-level=0 --width=512 --height=512 --torch-device=cpu --resume --inference
```

Finetuning step:

```
mlagents-learn PacManUnity/config/ft.yaml --force --run-id=EvaluationRun --torch-device=cpu --resume
mlagents-learn PacManUnity/config/ft.yaml --force --run-id=EvaluationRun --env='./PacManUnity/EvaluationBuilds/Game_5_Steps.exe' --quality-level=0 --width=512 --height=512 --torch-device=cpu --resume
mlagents-learn PacManUnity/config/ft.yaml --force --run-id=EvaluationRun --env='./PacManUnity/EvaluationBuilds/Game_10_Steps.exe' --quality-level=0 --width=512 --height=512 --torch-device=cpu --resume
mlagents-learn PacManUnity/config/ft.yaml --force --run-id=EvaluationRun --env='./PacManUnity/EvaluationBuilds/Game_15_Steps.exe' --quality-level=0 --width=512 --height=512 --torch-device=cpu --resume
```

Second evaluation step:

```
mlagents-learn PacManUnity/config/ft.yaml --force --run-id=EvaluationRun --torch-device=cpu --resume --inference
mlagents-learn PacManUnity/config/ft.yaml --force --run-id=EvaluationRun --env='./PacManUnity/EvaluationBuilds/Game_1000_Steps.exe' --quality-level=0 --width=512 --height=512 --torch-device=cpu --resume --inference
```
