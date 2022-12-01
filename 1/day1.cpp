#include <iostream>
#include <fstream>
#include <list>

using namespace std;

struct Elf;
void part1(list<Elf>);
void part2(list<Elf>);
list<Elf> process_input();

struct Elf {
    int id = 0;
    int total = 0;
    list<int> snacks;
};

// main func
int main() {

    list<Elf> elves = process_input();

    part1(elves);
    part2(elves);

    return 0;
}

//
// processes input, returns elves
//
list<Elf> process_input() {
    ifstream inputfile;
    inputfile.open("input", ios::in);

    if (!inputfile) { 
        cout << "Can't read input";
        exit(1);
    }

    list<Elf> elves;
    string line;

    Elf elf;
    int elfnum = 0;

    while (getline(inputfile, line)) {
        if (line.empty()) {
            int total = 0;

            for (auto snack : elf.snacks) 
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
    return elves;
}

//
// for sorting a list of Elves
//
bool compare_total (const Elf a, const Elf b) {
    return (a.total > b.total);
}

//
// part 1 solution
//
void part1(list<Elf> elves) {

    cout << "part 1" << endl << endl;

    elves.sort(compare_total);

    cout << "The elf with the most: " << elves.front().id << endl;
    cout << "They had a total of : " << elves.front().total << " energy" << endl;
    cout << endl;

}

//
// part 2 solution
//
void part2(list<Elf> elves) {
    cout << "part 2" << endl << endl;

    //sort by total (descending)
    elves.sort(compare_total);

    int top3total = 0;
    cout << "top three elves are: " << endl << endl;
    for(int i = 2; i >= 0; i--) {
        cout << "- elf #" << elves.front().id << " with " << elves.front().total << endl;
        top3total += elves.front().total;
        elves.pop_front();
    }

    cout << endl << "total of three: " << top3total << endl;
    cout << endl;

}

