const fs = require('fs');
const path = require('path');
const inputarr = fs.readFileSync(path.join(__dirname, './input.txt'), 'utf8').split('\n');

partOne();
partTwo();

function partOne() {
    let runningTotal = 0;
    const maxcubes = { 'red': 12, 'green': 13, 'blue': 14 }

    inputarr.forEach(function(value) {
        let [game, sets] = value.split(':').map(i => i.trim());
        let gameid = Number(game.match(/(\d+$)/)[0]);
        let set = sets.split(';').map(i => i.trim());
        let possible = true;

        set.forEach(function(value) {
            let cubes = value.split(",").map(i => i.trim());
            cubes.forEach(function(cube) {
                let [count, color] = cube.split(' ').map(i => i.trim());
                if (Number(count) > maxcubes[color]) { possible = false; }
            });
        });

        if (possible) { runningTotal += gameid; }
    });
    
    console.log(`Part One: ${runningTotal}`);
}

function partTwo() {
    let runningTotal = 0;

    inputarr.forEach(function(value) {
        let [ , sets] = value.split(':').map(i => i.trim());
        let set = sets.split(';').map(i => i.trim());
        let cubeMinimum = { 'red': 0, 'green': 0, 'blue': 0 }

        set.forEach(function(value) {
            let cubes = value.split(",").map(i => i.trim());
            cubes.forEach(function(cube) {
                let [count, color] = cube.split(' ').map(i => i.trim());
                if (cubeMinimum[color] < Number(count)) { cubeMinimum[color] = Number(count); }
            });
        });

        runningTotal += Object.values(cubeMinimum).reduce( (p, n) => p*n, 1); 
    });

    console.log(`Part Two: ${runningTotal}`);
}