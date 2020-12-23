namespace Sortable.Tests
{
    using System;
    using Xunit;

    public class SortableTest
    {
        [Fact]
        public void MergeSort_SHOULD_ORDER_GIVEN_UNORDERED()
        {
            // arrange
            var unsorted = new [] { 3, 25, 14, 36, 26, 17, 9, 25, 4 };
            // act
            Sortable.MergeSort(unsorted, 0, unsorted.Length - 1);
            // assert
            Assert.Equal(new [] { 3, 4, 9, 14, 17, 25, 25, 26, 36 }, unsorted);
        }

        [Fact]
        public void BubbleSort_SHOULD_ORDER_GIVEN_UNORDERED()
        {
            // arrange
            var unsorted = new [] { 3, 25, 14, 36, 26, 17, 9, 25, 4 };
            // act
            Sortable.BubbleSort(unsorted);
            // assert
            Assert.Equal(new [] { 3, 4, 9, 14, 17, 25, 25, 26, 36 }, unsorted);
        }

        [Fact]
        public void QuickSort_SHOULD_ORDER_GIVEN_UNORDERED()
        {
            // arrange
            var unsorted = new [] { 3, 25, 14, 36, 26, 17, 9, 25, 4 };
            // act
            Sortable.QuickSort(unsorted, 0, unsorted.Length - 1);
            // assert
            Assert.Equal(new [] { 3, 4, 9, 14, 17, 25, 25, 26, 36 }, unsorted);
        }

        [Fact]
        public void MergeSortExtensions_SHOULD_ORDER_GIVEN_UNORDERED()
        { 
            // arrange
            var unsorted = new [] { 3, 25, 14, 36, 26, 17, 9, 25, 4 };
            Func<int, int, bool> comparator = (x, y) => x > y;
            // act
            unsorted.MergeSort(comparator);
            // assert
            Assert.Equal(new [] { 3, 4, 9, 14, 17, 25, 25, 26, 36 }, unsorted);
        }

        [Fact]
        public void BubbleSortExtensions_SHOULD_ORDER_GIVEN_UNORDERED()
        {
            // arrange
            var unsorted = new [] { 3, 25, 14, 36, 26, 17, 9, 25, 4 };
            Func<int, int, bool> comparator = (x, y) => x > y;
            // act
            unsorted.BubbleSort(comparator);
            // assert
            Assert.Equal(new [] { 3, 4, 9, 14, 17, 25, 25, 26, 36 }, unsorted);
        }

        [Fact]
        public void QuickSortExtensions_SHOULD_ORDER_GIVEN_UNORDERED()
        {
            // arrange
            var unsorted = new [] { 3, 25, 14, 36, 26, 17, 9, 25, 4 };
            Func<int, int, bool> comparator = (a, b) => a > b;
            // act
            unsorted.QuickSort(comparator);
            // assert
            Assert.Equal(new [] { 3, 4, 9, 14, 17, 25, 25, 26, 36 }, unsorted);
        }
    }
}
