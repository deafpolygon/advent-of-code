#include <fstream>
#include <iostream>
#include <sstream>
#include <vector>

using namespace std;

struct Elf;
struct ElfGroup;
void part1(const vector<ElfGroup> &);
void part2(const vector<ElfGroup> &);
vector<ElfGroup> process_input(const string &);

struct Elf {
  int min;
  int max;
};

struct ElfGroup {
  vector<Elf> elves;
};

int main() {
  vector<ElfGroup> groups = process_input("input.txt");
  part1(groups);
  part2(groups);
  return 0;
}

vector<ElfGroup> process_input(const string &fname) {
  ifstream in;
  in.open(fname);
  if (!in) {
    cout << "Can't read input: " << fname;
    exit(1);
  }

  string ln;
  vector<ElfGroup> groups = {};

  while (getline(in, ln)) {
    stringstream ss(ln);
    vector<string> tokens;
    string tok;
    while (getline(ss, tok, ',')) {
      tokens.push_back(tok);
    }

    int tpos, npos = 0;
    ElfGroup pair;
    for (const auto &t : tokens) {
      stringstream tt(t);
      string numpart;
      Elf tempelf;
      while (getline(tt, numpart, '-')) {
        int num = stoi(numpart);
        if (npos == 0) {
          tempelf.min = num;
        } else if (npos == 1) {
          tempelf.max = num;
        } else {
          cout << "warning: splitting range has extra numbers\n";
        }
        npos++;
      }
      npos = 0;
      pair.elves.push_back(tempelf);
      tpos++;
    }
    groups.push_back(pair);
  }

  in.close();

  return groups;
}

void part1(const vector<ElfGroup> &groups) {
  int count = 0;
  for (const auto &pair : groups) {
    auto min1 = pair.elves[0].min;
    auto max1 = pair.elves[0].max;
    auto min2 = pair.elves[1].min;
    auto max2 = pair.elves[1].max;

    if (((min1 <= min2) && (max2 <= max1)) ||
        ((min2 <= min1) && (max1 <= max2))) {
      count++;
    }
  }
  cout << "p1: " << count << endl;
}

void part2(const vector<ElfGroup> &groups) {
  int count = 0;
  for (const auto &pair: groups) { 
    auto min1 = pair.elves[0].min;
    auto max1 = pair.elves[0].max;
    auto min2 = pair.elves[1].min;
    auto max2 = pair.elves[1].max;

    if(! ( (max1 < min2) || (min1 > max2) ) ) {
      count++;
    }
  }
  cout << "p2: " << count << endl;
}
