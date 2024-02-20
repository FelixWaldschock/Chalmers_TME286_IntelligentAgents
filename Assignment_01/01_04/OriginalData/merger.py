# merge all the .txt files in this folder into one large .txt file

import os
import sys

# in the folder "OriginalData", merge all the .txt files into one large .txt file
def merger():
    # get the current working directory
    cwd = os.getcwd()
    # change the working directory to "OriginalData"
    os.chdir(cwd + '/OriginalData')
    # get the current working directory
    cwd = os.getcwd()
    # get the list of all the files in the current working directory
    files = os.listdir(cwd)
    # create a new file to store the merged data
    with open('mergedData.txt', 'w') as outfile:
        # for each file in the current working directory
        for file in files:
            # if the file is a .txt file
            if file.endswith('.txt'):
                # open the file
                with open(file, 'r') as infile:
                    # read the content of the file
                    content = infile.read()
                    # write the content to the new file
                    outfile.write(content)
                    # add a new line to separate the content of different files
                    outfile.write('\n')
    # change the working directory back to the original directory
    os.chdir(cwd)
    return

if __name__ == '__main__':
    merger()
    print('Merging complete.')
    sys.exit(0)