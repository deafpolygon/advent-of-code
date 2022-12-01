#include <iostream>
#include <fstream>
#include <list>
#include <string>

using namespace std;

//declare stuff
struct Elf;
void part1(list<Elf>);
void part2(list<Elf>);
list<Elf> process_input();

// main func
int main() {

    list<Elf> elves = process_input();
//    part1(elves);
    part2(elves);

    return 0;
}

struct Elf {
    int id;
    list<int> snacks;
};


// processes input, returns elves
list<Elf> process_input() {

    list<Elf> elves;

    ifstream inputfile;
    string line;

    inputfile.open("input", ios::in);

    //read files
    if (!inputfile) { //if file doesn't exist
        cout << "No such file!";
    }
    else {
        int elfnum = 0;

        Elf elf;
        elf.id = elfnum;
        while (getline(inputfile, line)) {

            if (line == "") {
                //add the pre-existing elf to the list
                elves.push_back(elf);

                //make a new elf
                elfnum = elfnum + 1;
                Elf temp;
                temp.id = elfnum;
                elf = temp; //replace previous elf
                
            }
            else {
                //convert line into an integer
                int calorie = stoi(line);
                elf.snacks.push_back(calorie);
            }

        }
    }
    inputfile.close();
    return elves;
}

// part 1 solution
void part1(list<Elf> elves) {

    cout << "Part One" << endl << endl;

    int elf_with_the_most = -1;
    int maximum = 0;
    for (auto const& elf : elves) {

        int total = 0;
        for (auto const& energy : elf.snacks) {
            total = total + energy;
        }
        if (total > maximum) {
            maximum = total;
            elf_with_the_most = elf.id;
        }
    }

    cout << "The elf with the most: " << elf_with_the_most << endl;
    cout << "They had a total of : " << maximum << " energy" << endl;

}

// part 2 solution
void part2(list<Elf> elves) {
    cout << "part 2" << endl << endl;

    int ranked [elves.size()];
    
    for (auto const& elf : elves) {


    }



}

