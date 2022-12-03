#include <algorithm>
#include <fstream>
#include <iostream>
#include <list>
#include <sstream>
#include <string>
#include <tuple>
#include <vector>

using namespace std;
vector<tuple<string, string>> process_input();
void part1(const vector<tuple<string, string>>& data);
void part2(const vector<tuple<string, string>>& data);

// main func
int main() {
    auto data = process_input();
    part1(data);
    part2(data);
    return 0;
}

vector<tuple<string, string>> process_input() {
    ifstream inputfile;
    inputfile.open("input", ios::in);

    if (!inputfile) {
        cout << "Can't read input";
        exit(1);
    }

    tuple<string, string> play;
    vector<tuple<string, string>> data;

    string line;
    while (getline(inputfile, line)) {
        string opponent, play;
        stringstream s(line);
        s >> opponent >> play;
        data.push_back(make_tuple(opponent, play));
    }

    return data;
}

int result(const string& opponent, const string& play) {
    vector<string> prs = {"A", "B", "C"};
    auto* p1 = find(prs.front(), prs.back(), opponent);
    rotate(prs.begin(), prs.begin() - p1, prs.end());

    if (play == prs[1])
        return 0;
    else if (play == prs[2])
        return 6;
    else
        return 3;
}

void part1(const vector<tuple<string, string>>& data) {
    // A, X = rock - 1pt
    // B, Y = paper - 2pt
    // C, Z = scissor - 3pt
    // loss = 0pt, draw = 3pt, win = 6pt
    int score = 0;

    int* var;
    for (auto [opponent, play] : data) {
        // cout << "Opponent: " << opponent << " You: " << play << endl;
        if (play == "X") {
            score += 1;
            if (opponent == "A")
                score += 3;
            else if (opponent == "B")
                score += 0;
            else if (opponent == "C")
                score += 6;
        } else if (play == "Y") {
            score += 2;
            if (opponent == "A")
                score += 6;
            else if (opponent == "B")
                score += 3;
            else if (opponent == "C")
                score += 0;
        } else if (play == "Z") {
            score += 3;
            if (opponent == "A")
                score += 0;
            else if (opponent == "B")
                score += 6;
            else if (opponent == "C")
                score += 3;
        }
    }
    cout << "part 1 score: " << score << endl;
}

void part2(const vector<tuple<string, string>>& data) {
    // A, X = rock - 1pt
    // B, Y = paper - 2pt
    // C, Z = scissor - 3pt
    // loss = 0pt, draw = 3pt, win = 6pt
    int score = 0;
    for (auto [opponent, play] : data) {
        // cout << "Opponent: " << opponent << " You: " << play << endl;
        if (play == "X") {  // lose

            score += 0;
            if (opponent == "A") {  // rock - scissor
                score += 3;
            } else if (opponent == "B") {  // paper - rock
                score += 1;
            } else if (opponent == "C") {  // scissor - paper
                score += 2;
            }

        } else if (play == "Y") {  // draw
            score += 3;
            if (opponent == "A")  // rock - rock
                score += 1;
            else if (opponent == "B")  // paper - paper
                score += 2;
            else if (opponent == "C")  // scissor - scissor
                score += 3;
        } else if (play == "Z") {  // win
            score += 6;
            if (opponent == "A")  // rock - paper
                score += 2;
            else if (opponent == "B")  // paper - scissor
                score += 3;
            else if (opponent == "C")  // scissor - rock
                score += 1;
        }
    }
    cout << "part 2 score: " << score << endl;
}
