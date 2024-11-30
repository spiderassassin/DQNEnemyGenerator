import scipy
import sys
import os
import csv
import matplotlib.pyplot as plt

def peaks(text, results, directory):
    infos = ["time", "pellets", "tension", "distance", "reward"]

    numPeaks = {}
    numPeaks["before"] = []
    numPeaks["after"] = []

    for key in results["before"].keys():
        for info in infos[1:]:
            plt.plot(results["before"][key]["time"], results["before"][key][info])
            text.write(info + " peaks of initial game " + key + "\n")
            text.write(str(scipy.signal.find_peaks(results["before"][key][info])))
            text.write("\n")
            plt.xlabel("time")
            plt.ylabel(info)
            plt.title("Graph for " + info + " vs. time from initial game " + key + " from " + directory)
            plt.savefig(os.path.join("eval//graphs//line", info + "-" + "-before-" + key + "-" +  directory.replace('/','-')))
            plt.clf()
    
    for key in results["after"].keys():
        for info in infos[1:]:
            plt.plot(results["after"][key]["time"], results["after"][key][info])
            text.write(info + " peaks of final game " + key + "\n")
            text.write(str(scipy.signal.find_peaks(results["after"][key][info])))
            text.write("\n")
            plt.xlabel("time")
            plt.ylabel(info)
            plt.title("Graph for " + info + " vs. time from final game " + key + " from " + directory)
            plt.savefig(os.path.join("eval//graphs//line", info + "-" + "-after-" + key + "-" +  directory.replace('/','-')))
            plt.clf()

def change(text, results, directory):
    infos = ["time", "pellets", "tension", "distance", "reward"]
    before = {}
    after = {}
    
    for info in infos:
        before[info] = []
        after[info] = []

    for key in results["before"].keys():
        values = results["before"][key]
        for info in infos:
            if len(values[info]) == 0:
                continue
            before[info].append(values[info][-1])
            
    for key in results["after"].keys():
        values = results["after"][key]
        for info in infos:
            if len(values[info]) == 0:
                continue
            after[info].append(values[info][-1])

    for info in infos:
        plt.boxplot(before[info])
        plt.ylabel(info)
        plt.savefig(os.path.join("eval//graphs//boxplots",directory.replace('/','-')+"before"))
        plt.clf()
        plt.boxplot(after[info])
        plt.ylabel(info)
        plt.savefig(os.path.join("eval//graphs//boxplots",directory.replace('/','-')+"after"))
        plt.clf()

        text.write("change in " + info + "\n")
        text.write(str(scipy.stats.ttest_ind(before[info], after[info])))
        text.write("\n")

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
    logNum = 0
    for logFile in logs:
        if logNum == 0:
            state = "before"
        else:
            state = "after"
        results[state] = {}
        log = open(os.path.join(directory, logFile), 'r')
        currNum = -1
        values = csv.reader(log)
        lineNum = 0
        for line in values:
            if lineNum == 0:
                lineNum += 1
                continue
            if line[0][:4] == "Game":
                currNum += 1
                results[state][str(currNum)] = {}
                for info in infos:
                    results[state][str(currNum)][info] = []
                continue

            results[state][str(currNum)]["time"].append(float(line[0]))
            results[state][str(currNum)]["pellets"].append(float(line[1]))
            results[state][str(currNum)]["tension"].append(float(line[2]))
            results[state][str(currNum)]["distance"].append(float(line[3]))
            results[state][str(currNum)]["reward"].append(float(line[4]))
        log.close()
        logNum += 1

    return results

if __name__ == "__main__":
    directory = sys.argv[1]

    text = open("eval//stats//" + directory.replace('/','-') + ".txt", 'w')
    text.write("")
    text.close
    text = open("eval//stats//" + directory.replace('/','-') + ".txt", 'a')

    results = gatherResults(directory)

    peaks(text, results, directory)

    change(text, results, directory)

    text.close()