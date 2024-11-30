import scipy
import sys
import os
import csv
import matplotlib.pyplot as plt

def peaks(number, results, directory):
    infos = ["time", "pellets", "tension", "distance", "reward"]

    for i in range(number):
        for info in infos[1:]:
            plt.scatter(results[str(i)]["time"], results[str(i)][info])
            print(scipy.signal.find_peaks(results[str(i)][info]))
            plt.savefig(info + " " + str(i) + " " +  directory.replace('/','-'))

def change(number, results, directory):
    infos = ["time", "pellets", "tension", "distance", "reward"]
    before = {}
    after = {}
    
    for info in infos:
        before[info] = []
        after[info] = []

    for i in range(int(number/2)):
        values = results[str(i)]
        for info in infos:
            before[info].append(values[info][-1])
            
    for i in range(int(number/2), number):
        values = results[str(i)]
        for info in infos:
            after[info].append(values[info][-1])

    for info in infos:
        plt.boxplot(before[info])
        plt.savefig(directory)
        plt.boxplot(after[info])
        plt.savefig(directory)
        print(scipy.stats.ttest_ind(before[info], after[info]))

# number is the amount of logs we have (assume first half is first test, second half is second test)
def gatherResults(directory):
    logs = [0,0]
    infos = ["time", "pellets", "tension", "distance", "reward"]
    for i in os.listdir(directory):
        if i.find("initial") > -1:
            logs[0] = i
        elif i.find("final") > -1:
            logs[1] = i

    # for i in range(number):
    #     logs.append(open("log" + str(i) + ".csv", "r"))

    results = {}
    currNum = 0

    for logFile in logs:
        results[str(currNum)] = {}
        for info in infos:
            results[str(currNum)][info] = []
        log = open(os.path.join(directory, logFile), 'r')
        values = csv.reader(log)
        print(values)
        prev_tension = 0
        prev_pellets = 480810580
        lineNum = 0
        for line in values:
            if lineNum == 0:
                lineNum += 1
                continue
            print(line)
            if float(line[1]) > prev_pellets:
                currNum += 1
                results[str(currNum)] = {}
                for info in infos:
                    results[str(currNum)][info] = []

            results[str(currNum)]["time"].append(float(line[0]))
            results[str(currNum)]["pellets"].append(float(line[1]))
            results[str(currNum)]["tension"].append(float(line[2]) - prev_tension)
            results[str(currNum)]["distance"].append(float(line[3]))
            results[str(currNum)]["reward"].append(float(line[4]))
            prev_tension = float(line[2])
            prev_pellets = float(line[1])
        log.close()

    return results

if __name__ == "__main__":
    directory = sys.argv[1]
    number = int(sys.argv[2])

    results = gatherResults(directory)

    peaks(number, results, directory)

    change(number, results, directory)