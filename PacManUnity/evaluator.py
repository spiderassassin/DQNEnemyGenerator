import scipy
import sys
import csv
import matplotlib.pyplot as plt

def peaks(number, results):
    infos = ["time", "pellets", "tension", "distance", "reward"]

    for i in range(number):
        for info in infos[1:]:
            plt.scatter(results[str(i)]["time"], results[str(i)][info])
            scipy.find_peaks(results[str(i)][info])

def change(number, results):
    infos = ["time", "pellets", "tension", "distance", "reward"]
    before = {}
    after = {}
    
    for info in infos:
        before[info] = []
        after[info] = []

    for i in range(number/2):
        values = results[str(i)]
        for info in infos:
            before[info].extend(values[str(i)][info])
            
    for i in range(number/2, number):
        values = results[str(i)]
        for info in infos:
            after[info].extend(values[str(i)][info])

    for info in infos:
        plt.boxplot(before[info])
        plt.boxplot(after[info])
        scipy.stats.ttest_ind(before[info], after[info])

# number is the amount of logs we have (assume first half is first test, second half is second test)
def gatherResults(number):
    logs = []
    for i in range(number):
        logs.append(open("log" + str(i) + ".csv", "r"))

    results = {}
    currNum = 0

    for log in logs:
        results[str(currNum)] = {}
        values = csv.reader(log)
        for line in values:
            results[str(currNum)]["time"] = line[0]
            results[str(currNum)]["pellets"] = line[1]
            results[str(currNum)]["tension"] = line[2]
            results[str(currNum)]["distance"] = line[4]
            results[str(currNum)]["reward"] = line[5]
        log.close()

    return results

if __name__ == "__main__":
    mode = sys.argv[1]
    number = sys.argv[2]

    results = gatherResults(number)

    peaks(number, results)

    change(number, results)