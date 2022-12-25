#include <algorithm>
#include <chrono>
#include <fstream>
#include <iomanip>
#include <iostream>
#include <sstream>
#include <tuple>
#include <vector>

using namespace std;
void process_input(string);
bool testunique(vector<char>);

int main() {
  process_input("input.txt");
}

bool testunique(vector<char> comm) {
  for (uint i = 0; i < comm.size(); i++) {
    for (uint j = i + 1; j < comm.size(); j++) {
      if (comm[i] == comm[j]) {
        return false;
      }
    }
  }
  return true;
}

string process_input(string fn) {
  ifstream in(fn);
  stringstream buffer;
  buffer << in.rdbuf();
  return buffer.str();

  int pos = 0;
  vector<char> comm;
  for (auto ch : buffer.str()) {
    comm.insert(comm.begin(), ch);
    if (pos >= 3) {
      cout << "sequence: " << comm[3] << comm[2] << comm[1] << comm[0] << endl;
      if (testunique(vector<char>{ comm.begin(), comm.begin()+14})) {
        cout << pos + 1 << " position unique." << endl;
        break;
      }
    }

    pos++;
  }
}
