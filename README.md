# DQNEnemyGenerator

### Running evaluations (with script)

- First, make sure you're in the base directory (DQNEnemyGenerator).

- Then, ensure there are no log.csv files in the directory (eg. log.csv, log2025-04-24-10-50-30.csv, etc.).

- In run_evaluation.py, set the number of iterations (i.e. number of trials to perform).

- When ready, run the script:

```
python3 run_evaluation.py
```

- Results will be saved in the `results_evaluation/rlc_results/` folder.

### Running evaluations manually (with build)

Run 1000 evaluations, then run 5/10/15 finetuning steps, then run 1000 more evaluations.

First evaluation step:

```
mlagents-learn PacManUnity/config/ft.yaml --force --run-id=EvaluationRun --env='./PacManUnity/EvaluationBuilds/Game_1000_Steps.exe' --quality-level=0 --width=512 --height=512 --torch-device=cpu --max-lifetime-restarts=0 --resume --inference
```

Finetuning step:

```
mlagents-learn PacManUnity/config/ft.yaml --force --run-id=EvaluationRun --env='./PacManUnity/EvaluationBuilds/Game_5_Steps.exe' --quality-level=0 --width=512 --height=512 --torch-device=cpu --max-lifetime-restarts=0 --resume
mlagents-learn PacManUnity/config/ft.yaml --force --run-id=EvaluationRun --env='./PacManUnity/EvaluationBuilds/Game_10_Steps.exe' --quality-level=0 --width=512 --height=512 --torch-device=cpu --max-lifetime-restarts=0 --resume
mlagents-learn PacManUnity/config/ft.yaml --force --run-id=EvaluationRun --env='./PacManUnity/EvaluationBuilds/Game_15_Steps.exe' --quality-level=0 --width=512 --height=512 --torch-device=cpu --max-lifetime-restarts=0 --resume
```

Second evaluation step:

```
mlagents-learn PacManUnity/config/ft.yaml --force --run-id=EvaluationRun --env='./PacManUnity/EvaluationBuilds/Game_1000_Steps.exe' --quality-level=0 --width=512 --height=512 --torch-device=cpu --max-lifetime-restarts=0 --resume --inference
```

### Running evaluations manually (in editor)

Run 1000 evaluations, then run 5/10/15 finetuning steps, then run 1000 more evaluations.

First evaluation step:

```
mlagents-learn PacManUnity/config/ft.yaml --force --run-id=EvaluationRun --torch-device=cpu --resume --inference
```

Finetuning step:

```
mlagents-learn PacManUnity/config/ft.yaml --force --run-id=EvaluationRun --torch-device=cpu --resume
```

Second evaluation step:

```
mlagents-learn PacManUnity/config/ft.yaml --force --run-id=EvaluationRun --torch-device=cpu --resume --inference
```

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
