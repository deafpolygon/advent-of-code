#include <algorithm>
#include <fstream>
#include <iostream>
#include <map>
#include <sstream>
#include <string>
#include <vector>

using namespace std;

int main() {
  string fn = "input.txt";

  vector<int> abc;
  int num = abc;

  ifstream in;
  in.open(fn);
  if (!in) {
    cout << "Can't open file: " << fn;
    exit(1);
  }

  vector<string> directories;
  vector<string> files;

  cout << "hello, testing" << endl;

  vector<string> currentdir;

  string line;
  while (getline(in, line)) {
    cout << line << endl;
  }

  return 0;
}
