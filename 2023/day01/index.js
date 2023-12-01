const fs = require('fs');
const path = require('path');
const input = fs.readFileSync(path.join(__dirname, './input.txt'), 'utf8');
const validNumberString = ['one', 'two', 'three', 'four', 'five', 'six', 'seven', 'eight', 'nine'];

partOne();
partTwo();

function getNumberPair(value) {
    let numOnly = value.replace(/\D/g, "");
    return Number(numOnly.slice(0,1) + numOnly.slice(-1));
}

function filterStringForPuzzle(value) {    
    let processing = value;
    //bit of a cheat, but it works for this puzzle
    validNumberString.forEach(function(validnum, idx) {
        const replacement = `${validnum.slice(0,1)}${idx+1}${validnum.slice(-1)}`;
        processing = processing.replaceAll(validnum, replacement);
    });
    return processing;
}

function add(accumulator, a) {
    return accumulator + Number(a);
}

function partOne() {
    const inputarr = input.split('\n');
    console.log(inputarr.map(getNumberPair).reduce(add));
}

function partTwo() {
    const inputarr = input.split('\n');
    console.log(inputarr.map(filterStringForPuzzle).map(getNumberPair).reduce(add));
}
