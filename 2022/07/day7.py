#!/usr/bin/env python
import pprint

def main():
    with open('input.txt', 'r') as f: 
        lines = f.readlines()

    print(lines)

    pwd = [] #track present path
    list_mode = False

    paths = []
    files = {}

    for ln in lines:

        tokens = ln.strip().split(' ')

        if (tokens[0] == '$'):

            # ls done if command is next
            if list_mode:
                list_mode = False
            # cd ".." | cd "path"
            if (tokens[1] == 'cd'):
                if(tokens[2] == '..'):
                    pwd.pop()
                else:
                    pwd.append(tokens[2])
                    paths.append('/'.join(pwd))
            # ls 'path'
            elif (tokens[1] == 'ls'):
                list_mode = True

        elif list_mode:
            if(tokens[0] == 'dir'):
                pass #dont know?
            else:
                fn = '/'.join(paths) + '/' + tokens[1]
                files[fn] = tokens[0] #track sizes


    pprint.pprint(paths)
    #print(files)

 
if __name__ == "__main__":
    main()
    