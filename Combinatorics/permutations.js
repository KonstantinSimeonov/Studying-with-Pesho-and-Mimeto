function getPermutationsArray(array) {

	var permutations = [],
		used = {};

	function swap(arr, i, j) {

		if ((i === j) || (arr[i] == arr[j])) {
			return;
		}

		var c = arr[i];
		arr[i] = arr[j];
		arr[j] = c;
	}
	
	function pushPermutations(arr, start, end) {

		if (start === end) {
			
			if(used[arr.toString()]) {
				return;
			}
			
			permutations.push(arr);
			used[arr.toString()] = true;
			
			return;
		}
		
		for (var i = start; i <= end; i++) {
			
			var copiedArray = arr.slice();
			swap(copiedArray, start, i);
			
			pushPermutations(copiedArray, start + 1, end);
		}
	}

	pushPermutations(array, 0, array.length - 1);

	return permutations;
}

var test1 = [1, 2, 2],
	test2 = [1, 1, 1],
	test3 = [1, 2, 3, 4];


console.log(getPermutationsArray(test1));