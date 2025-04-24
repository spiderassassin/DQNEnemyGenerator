import shutil
import subprocess
import glob
import os
import time

ITERATIONS = 10

def main():
    results_dir = f"results_evaluation/rlc_results/{time.strftime('%Y-%m-%d_%H-%M-%S')}"
    for i in range(ITERATIONS):
        print(f"Iteration {i + 1}/{ITERATIONS}")
        for ft_steps in [5, 10, 15]:
            results_path = f"{results_dir}/trial_{i + 1}/{ft_steps}"
            source = "results/TestNewActions1"
            destination = "results/EvaluationRun"

            # Make a clean copy of the run folder.
            shutil.rmtree(destination, ignore_errors=True)
            shutil.copytree(source, destination)

            # First evaluation.
            command = [
                "mlagents-learn",
                "PacManUnity/config/ft.yaml",
                "--force",
                "--run-id=EvaluationRun",
                "--env=./PacManUnity/EvaluationBuilds/Game_1000_Steps.exe",
                "--quality-level=0",
                "--width=512",
                "--height=512",
                "--torch-device=cpu",
                "--max-lifetime-restarts=0",
                "--resume",
                "--inference"
            ]
            subprocess.run(command, check=True)

            # Find the log file.
            log_file = glob.glob("log*.csv")[0]  # Make sure there's only one.
            os.makedirs(results_path, exist_ok=True)
            # Save it.
            shutil.copy(log_file, f"{results_path}/initial.csv")
            # Delete it.
            os.remove(log_file)

            # Finetuning.
            command = [
                "mlagents-learn",
                "PacManUnity/config/ft.yaml",
                "--force",
                "--run-id=EvaluationRun",
                f"--env=./PacManUnity/EvaluationBuilds/Game_{ft_steps}_Steps.exe",
                "--quality-level=0",
                "--width=512",
                "--height=512",
                "--torch-device=cpu",
                "--max-lifetime-restarts=0",
                "--resume",
            ]
            subprocess.run(command, check=True)

            # Find the log file.
            log_file = glob.glob("log*.csv")[0]  # Assumes there's only one.
            os.makedirs(results_path, exist_ok=True)
            # Save it.
            shutil.copy(log_file, f"{results_path}/finetuning.csv")
            # Delete it.
            os.remove(log_file)

            # Second evaluation.
            command = [
                "mlagents-learn",
                "PacManUnity/config/ft.yaml",
                "--force",
                "--run-id=EvaluationRun",
                "--env=./PacManUnity/EvaluationBuilds/Game_1000_Steps.exe",
                "--quality-level=0",
                "--width=512",
                "--height=512",
                "--torch-device=cpu",
                "--max-lifetime-restarts=0",
                "--resume",
                "--inference"
            ]
            subprocess.run(command, check=True)

            # Find the log file.
            log_file = glob.glob("log*.csv")[0]  # Make sure there's only one.
            os.makedirs(results_path, exist_ok=True)
            # Save it.
            shutil.copy(log_file, f"{results_path}/final.csv")
            # Delete it.
            os.remove(log_file)

            # Remove the copied run folder.
            shutil.rmtree(destination)

if __name__ == "__main__":
    main()
