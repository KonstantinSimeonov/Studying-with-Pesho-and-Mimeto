function getCombinationsArray(array) {

	var combinations = [];

	function swap(arr, i, j) {

		if ((i === j) || (arr[i] == arr[j])) {
			return;
		}

		var c = arr[i];
		arr[i] = arr[j];
		arr[j] = c;
	}

	function pushCombinations(arr, start, end) {

		// base case: one-element sequence is the only possible combination
		if (start === end) {
			combinations.push(arr);
			return;
		}
		
		// if more than one element, for every element in the sequence perform the following:
		// take it as the first element and calculate the permutations of the other elements
		for (var i = start; i <= end; i++) {
			
			// take another element as first from the sequence
			var copiedArray = arr.slice();
			swap(copiedArray, start, i);
			
			// calculate it's combinations
			pushCombinations(copiedArray, start + 1, end);
		}
	}

	pushCombinations(array, 0, array.length - 1);

	return combinations;
}

var test1 = [1, 2, 3, 4],
	test2 = [0, 1, 2],
	test3 = [1, 2, 3, 4, 5, 6, 7, 8];


console.log(getCombinationsArray(test1));