// Maybe useful functions

export function isNumber(n) { 
    return !isNaN(parseFloat(n)) && !isNaN(n - 0);
}
