#include <algorithm>
#include <fstream>
#include <iostream>
#include <regex>
#include <sstream>
#include <tuple>
#include <vector>

using namespace std;
vector<string> split(const string &, char);
tuple<vector<vector<char>>, vector<string>> process_input(const string &);
void part1(vector<vector<char>>, vector<string>);
void part1b(vector<vector<char>>, vector<string>);
void part2(vector<vector<char>>, vector<string>);

int main() {
  auto [stacks, commands] = process_input("input.txt");
  part1b(stacks, commands);
  part2(stacks, commands);
  return 0;
}

vector<string> split(const string &str, char delim) {
  string temp;
  stringstream ss(str);
  vector<string> result;

  while (getline(ss, temp, delim)) {
    result.push_back(temp);
  }
  return result;
}

tuple<vector<vector<char>>, vector<string>> process_input(const string &fname) {
  ifstream in;
  in.open(fname);
  if (!in) {
    cout << "Can't read input: " << fname;
    exit(1);
  }

  int mode = 0;              // reading mode
  vector<string> cratedata;  // mode 0
  vector<string> movedata;   // mode 1
  string ln;
  while (getline(in, ln)) {
    if (ln.empty()) {
      mode++;
      continue;
    } else if (mode == 0) {
      cratedata.insert(cratedata.begin(), ln);  // reverse order
    } else if (mode == 1) {
      movedata.push_back(ln);
    } else {
      cout << "warning: issue handling file";
    }
  }

  int linepos = 0;
  vector<vector<char>> stacks;
  for (const auto &line : cratedata) {
    int vecpos = 0;
    for (uint i = 1; i < line.size(); i = i + 4) {
      if (linepos == 0)
        stacks.push_back(vector<char>{});  // initialize stacks

      if (isalnum(line[i]))
        stacks[vecpos].push_back(line[i]);
      vecpos++;
    }
    vecpos = 0;
    linepos++;
  }

  return tuple{stacks, movedata};
}

// For some reason, doing regex in C++ is really slow.
//
//void part1(vector<vector<char>> stacks, vector<string> commands) {
//  // part1
//  for (const auto &cmd : commands) {
//    regex regex(R"(^\w+\s(\d+)\s\w+\s(\d+)\s\w+\s(\d+)$)");
//    smatch m;
//    if (regex_match(cmd.begin(), cmd.end(), m, regex)) {
//      int count = stoi(m[1]);
//      int from = stoi(m[2]) - 1;
//      int to = stoi(m[3]) - 1;
//      for (int i = 0; i < count; i++) {
//        char crate = stacks[from].back();
//        stacks[from].pop_back();
//        stacks[to].push_back(crate);
//      }
//    }
//  }
//
//  cout << "Stacks layout after parsing" << endl;
//  cout << "Length of stacks: " << stacks.size() << endl;
//  for (const auto &stack : stacks) {
//    for (const auto &crate : stack) {
//      cout << " " << crate;
//    }
//    cout << endl;
//  }
//}

void part1b(vector<vector<char>> stacks, vector<string> commands) {
  // part2
  for (const auto &cmd : commands) {
    vector<string> cmdsplit = split(cmd, ' ');
    int count = stoi(cmdsplit[1]);
    int from = stoi(cmdsplit[3]) - 1;
    int to = stoi(cmdsplit[5]) - 1;

    vector<char> subset{stacks[from].end() - count, stacks[from].end()};
    reverse(subset.begin(), subset.end());
    stacks[to].insert(stacks[to].end(), subset.begin(), subset.end());
    stacks[from].erase(stacks[from].end() - count, stacks[from].end());
  }

  cout << "Stacks layout after parsing" << endl;
  cout << "Length of stacks: " << stacks.size() << endl;
  for (const auto &stack : stacks) {
    for (const auto &crate : stack) {
      cout << " " << crate;
    }
    cout << endl;
  }
}

void part2(vector<vector<char>> stacks, vector<string> commands) {
  // part2
  for (const auto &cmd : commands) {
    vector<string> cmdsplit = split(cmd, ' ');
    int count = stoi(cmdsplit[1]);
    int from = stoi(cmdsplit[3]) - 1;
    int to = stoi(cmdsplit[5]) - 1;

    vector<char> subset{stacks[from].end() - count, stacks[from].end()};
    stacks[to].insert(stacks[to].end(), subset.begin(), subset.end());
    stacks[from].erase(stacks[from].end() - count, stacks[from].end());
  }

  cout << "Stacks layout after parsing" << endl;
  cout << "Length of stacks: " << stacks.size() << endl;
  for (const auto &stack : stacks) {
    for (const auto &crate : stack) {
      cout << " " << crate;
    }
    cout << endl;
  }
}
