namespace Sortable
{
    using System;
    using System.Collections;
    using System.Threading;
    using System.Xml.Xsl;

    public static class Sortable
    {
        public static void MergeSort(int[] arr, int start, int end)
        {
            // recursive base case
            if (start == end)
                return;
            
            // sort left + right
            var middle = start + (end - start) / 2;
            MergeSort(arr, start, middle);
            MergeSort(arr, middle + 1, end);
            // merge sorted halves
            Merge(arr, start, middle, end);
        }

        private static void Merge(int[] arr, int start, int middle, int end)
        {
            // initialize temporary array
            var sorted = new int[arr.Length];
            // initialize pointers for merging
            var left = start;
            var right = middle + 1;
            var idx = start;
            // add smallest from two halves in order
            while (left <= middle && right <= end)
            {
                sorted[idx++] = arr[left] < arr[right] ? arr[left++] : arr[right++];
            }
            // add in remaining elements from both arrays
            Array.Copy(arr, left, sorted, idx, middle - left + 1);
            Array.Copy(arr, right, sorted, idx, end - right + 1);
            // merge elements back in in sorted order
            Array.Copy(sorted, start, arr, start, end - start + 1);
        }

        public static void BubbleSort(int[] arr)
        {
            // local helper function for swapping two elements in an array
            Action<int, int> swap = (int a, int b) =>
            {
                var temp = arr[a];
                arr[a] = arr[b];
                arr[b] = temp;
            };
            // initialize stop condition
            bool swapped;
            do
            {
                // initialize stop condition if already sorted
                // this will stop loops
                swapped = false;
                for (var i = 0; i < arr.Length - 1; i++)
                {
                    // if left element is larger than right element
                    // bubble up the larger element one position
                    if (arr[i] > arr[i + 1])
                    {
                        swap(i, i + 1);
                        swapped = true;
                    }
                }

            } while (swapped);
        }

        public static void QuickSort(int[] arr, int left, int right)
        {
            // define recursive end condition
            if (left >= right)
                return;
            
            // define the pivot element as a random element
            // in the array
            var pivot = arr[new Random().Next(left, right)];
            // partition array into two parts using partition
            var prt_idx = Partition(arr, left, right, pivot);
            // sort around partition
            QuickSort(arr, left, prt_idx - 1);
            QuickSort(arr, prt_idx, right);
        }

        private static int Partition(int[] arr, int left, int right, int pivot)
        {
            // define local helper function to swap
            // two elements in an array
            Action<int, int > swap = (a, b) =>
            {
                var temp = arr[a];
                arr[a] = arr[b];
                arr[b] = temp;
            };
            // start swapping elements around pivot
            while (left <= right)
            {
                // run pointers along array until two elements
                // satisfying the pivot condition are met
                while (arr[left] < pivot) left++;
                while (arr[right] > pivot) right--;
                // once pivot elements are found swap them
                if (left <= right)
                {
                    swap(left, right);
                    // increment pointers after swapping
                    left++;
                    right--;
                }
            }

            return left;
        }
    }

    public static class Extensions
    {
        public static void MergeSort<T>(this IList src, Func<T, T, bool> comparator)
        {
            MergeSort(src, 0, src.Count - 1, comparator);
        }
    
        private static void MergeSort<T>(IList arr, int start, int end, Func<T, T, bool> comp)
        {
            // recursive end condition
            if (start == end)
                return;
    
            // sort left + right
            var middle = start + (end - start) / 2;
            MergeSort(arr, start, middle, comp);
            MergeSort(arr, middle + 1, end, comp);
            // merge two sorted halves
            Merge(arr, start, middle, end, comp);
        }
    
        private static void Merge<T>(IList arr, int start, int middle, int end, Func<T, T, bool> comp)
        {
            // initialize placeholder
            var sorted = new T[arr.Count];
            // set pointers
            var left = start;
            var right = middle + 1;
            var idx = start;
            // add elements into placeholder array in sort order
            while (left <= middle && right <= end)
            {
                // want to add smallest element
                // Comparison<T> 
                sorted[idx++] = comp((T) arr[left], (T) arr[right]) ? (T) arr[right++] : (T) arr[left++];
            }
    
            // copy remaining elements into sorted array
            while (left <= middle)
                sorted[idx++] = (T) arr[left++];
            while (right <= end)
                sorted[idx++] = (T) arr[right++];
            // copy sorted elements into original
            for (var i = start; i <= end; i++)
            {
                arr[i] = sorted[i];
            }
        }

        public static void BubbleSort<T>(this IList arr, Func<T, T, bool> comparator)
        {
            // helper function for swapping elements in place
            Action<IList, int, int> swap = (a, left, right) =>
            {
                var temp = a[left];
                a[left] = a[right];
                a[right] = temp;
            };
            bool swapped;
            do
            {
                // initialize stop condition
                swapped = false;
                for (var i = 0; i < arr.Count - 1; i++)
                {
                    // if left > right; swap them in place
                    if (comparator((T)arr[i], (T)arr[i + 1]))
                    {
                        swap(arr, i, i + 1);
                        swapped = true;
                    }
                }       
            } while (swapped);
        }

        public static void QuickSort<T>(this IList arr, Func<T, T, bool> comparator)
        {
            QuickSort(arr, 0, arr.Count - 1, comparator);
        }

        private static void QuickSort<T>(IList arr, int left, int right, Func<T, T, bool> comp)
        {
            // recursive end condition
            if (left >= right)
                return;
            
            // assign random pivot
            var pivot = (T)arr[new Random().Next(left, right)];
            // partition collection around pivot
            var prt_idx = Partition(arr, left, right, pivot, comp);
            // sort partitions independently
            QuickSort(arr, left, prt_idx - 1, comp);
            QuickSort(arr, prt_idx, right, comp);
        }

        private static int Partition<T>(IList arr, int left, int right, T pivot, Func<T, T, bool> comp)
        {
            // local helper function to swap elements
            Action<int, int> swap = (l, r) =>
            {
                var temp = arr[l];
                arr[l] = arr[r];
                arr[r] = temp;
            };
            // loop through swapping elements around pivot
            while (left <= right)
            {
                // move pointers until elements satisfy
                // sorting predicate
                while (comp(pivot, (T)arr[left]))
                    left++;
                while (comp((T)arr[right], pivot))
                    right--;

                if (left <= right)
                {
                    swap(left, right);
                    left++;
                    right--;
                }
            }

            return left;
        }
    }
}