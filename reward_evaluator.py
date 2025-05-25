import scipy
import sys
import os
import csv
import matplotlib.pyplot as plt
import numpy as np
from datetime import datetime

def gatherRewards(directory):
    logs = []
    for i in os.listdir(directory):
        logs.append(i)

    rewards = []
    for logFile in logs:
        log = open(os.path.join(directory, logFile), 'r')
        currNum = -1
        values = csv.reader(log)
        lineNum = 0
        currReward = 0
        for line in values:
            if lineNum == 0:
                lineNum += 1
                continue
            if line[0][:4] == "Game":
                currNum += 1
                rewards.append(currReward)
                continue

            if currNum > 1000:
                break

            currReward = float(line[4])
        log.close()

    return rewards

if __name__ == "__main__":
    directory_0 = sys.argv[1]
    directory_5 = sys.argv[2]
    directory_10 = sys.argv[3]
    directory_15 = sys.argv[4]


    reward_0 = gatherRewards(directory_0)
    mean_0 = np.mean(reward_0)
    std_0 = np.std(reward_0)

    reward_5 = gatherRewards(directory_5)
    mean_5 = np.mean(reward_5)
    std_5 = np.std(reward_5)

    reward_10 = gatherRewards(directory_10)
    mean_10 = np.mean(reward_10)
    std_10 = np.std(reward_10)

    reward_15 = gatherRewards(directory_15)
    mean_15 = np.mean(reward_15)
    std_15 = np.std(reward_15)

    timestamp = datetime.now().strftime("%Y-%m-%d_%H-%M-%S")
    text = open(f"eval//stats//{timestamp}.txt", 'a')
    text.write(f"0: {mean_0} +/- {std_0}\n")
    text.write(f"5: {mean_5} +/- {std_5}\n")
    text.write(f"10: {mean_10} +/- {std_10}\n")
    text.write(f"15: {mean_15} +/- {std_15}\n")
    text.close()

    # Plotting.
    x = [0, 5, 10, 15]
    y = [mean_0, mean_5, mean_10, mean_15]
    yerr = [std_0, std_5, std_10, std_15]
    plt.errorbar(x, y, yerr=yerr, fmt='o-', capsize=5)  # Use 'o-' to connect points with a line
    plt.title("Ghost Agent Learning Curve")
    plt.xlabel("Fine-tuning Steps")
    plt.ylabel("Average Reward")
    plt.xticks(x)
    plt.grid()
    # Ensure the directory exists before saving the plot
    os.makedirs("eval/plots", exist_ok=True)
    plt.savefig(f"eval//plots//{timestamp}.png")
    plt.close()
