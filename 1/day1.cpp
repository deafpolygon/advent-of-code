#include <iostream>
#include <fstream>
#include <vector>
#include <algorithm>
#include <list>

using namespace std;

struct Elf;
void part1(const vector<Elf> &elves);
void part2(const vector<Elf> &elves);
vector<Elf> process_input();

struct Elf {
    int id = 0;
    int total = 0;
    list<int> snacks;
};

// main func
int main() {
    vector<Elf> elves = process_input();
    part1(elves);
    part2(elves);
    return 0;
}

//
// processes input, returns elves
//
vector<Elf> process_input() {
    ifstream inputfile;
    inputfile.open("input", ios::in);

    if (!inputfile) {
        cout << "Can't read input"; 
        exit(1);
    }

    vector<Elf> elves;
    string line;
    Elf elf;
    int elfnum = 0;

    while (getline(inputfile, line)) {
        if (line.empty()) {
            int total = 0;

            for (const auto snack : elf.snacks) 
                total += snack;

            elf.total = total;
            elves.push_back(elf);

            elf = {}; //re-init
            elf.id = ++elfnum;
            continue;
        }
        elf.snacks.push_back( stoi(line) );
    }
    inputfile.close();

    sort(elves.begin(), elves.end(), [](auto a, auto b) { return (a.total > b.total); } );
    return elves;
}

//
// part 1 solution
//
void part1(const vector<Elf> &elves) {
    cout << "part 1" << endl << endl;
    cout << "The elf with the most: " << elves[0].id << endl;
    cout << "They had a total of : " << elves[0].total << " energy" << endl;
    cout << endl;
}

//
// part 2 solution
//
void part2(const vector<Elf> &elves) {
    cout << "part 2" << endl << endl;
    int top3total = 0;
    cout << "top three elves are: " << endl << endl;
    for(int i = 0; i < 3; i++) {
        top3total += elves[i].total;
        cout << "- elf #" << elves[i].id << " with " << elves[i].total << endl;
    }
    cout << endl << "total of three: " << top3total << endl;
    cout << endl;
}

