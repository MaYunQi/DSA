
namespace DSA.Algorithms.Sort
{
    public static class SortHelper<T> where T: IComparable<T>,IEquatable<T>
    {
        public static void BubbleSort(T[] array)
        { 
            if(array==null||array.Length<=1)
                return;
            bool isSwaped;
            int n=array.Length;
            for(int i=0;i< n; i++)
            {
                isSwaped = false;
                for (int j=0;j< n - 1-i; j++)
                {
                    if (array[j].CompareTo(array[j+1])>0)
                    {
                        T temp = array[j+1];
                        array[j+1] = array[j];
                        array[j] = temp;
                        isSwaped = true;
                    }
                }
                if (!isSwaped)
                    return;
            }
        }
        public static void InsertionSort(T[] array)
        {
            if(array==null||array.Length<=1)
                return;
            int n=array.Length;
            for(int i=1;i<n;i++)
            {
                T key=array[i];
                int j=i-1;
                while (j >= 0 && array[j].CompareTo(key)>0)
                {
                    array[j+1] = array[j];
                    j--;
                }
                array[j+1] = key;
            }
        }
        public static void SelectionSort(T[] array)
        {
            if (array == null || array.Length <= 1)
                return;
            int n = array.Length;
            for (int i = 0; i < n -1; i++)
            {
                int minValueIndex = i;
                for (int j = i + 1; j < n; j++)
                {
                    if (array[j].CompareTo(array[minValueIndex]) < 0)
                        minValueIndex = j;
                }
                if (minValueIndex != i)
                {
                    T temp = array[minValueIndex];
                    array[minValueIndex] = array[i];
                    array[i] = temp;
                }
            }
        }
        public static void QuickSort(T[] array)
        {
            if (array == null || array.Length <= 1)
                return;
            int n=array.Length-1;
            Random random = new Random();
            QuickSort(array, 0, n, random);
        }
        private static int Partition(T[] array, int left, int right, int pivot)
        {
            T pivotValue = array[pivot];

            T temp = array[right];
            array[right] = array[pivot];
            array[pivot] = temp;

            pivot = right;  
            right--;
            while (left <= right)
            {
                while (left <= right && array[left].CompareTo(pivotValue) < 0)
                {
                    left++;
                }
                while (left <= right && array[right].CompareTo(pivotValue) > 0)
                {
                    right--;
                }
                if (left <= right)
                {
                    temp = array[right];
                    array[right] = array[left];
                    array[left] = temp;
                    left++;
                    right--;
                }
            }
            temp = array[left];
            array[left] = array[pivot];
            array[pivot] = temp;
            return left;
        }
        private static void QuickSort(T[] array,int left,int right,Random rnd)
        {
            if (left >= right)
                return;
            int pivot= rnd.Next(left, right+1);
            int newPivot=Partition(array, left,right ,pivot);
            QuickSort(array, left, newPivot-1,rnd);
            QuickSort(array, newPivot+1, right, rnd);
        }
        public static void MergeSort(T[] array)
        {
            if(array==null||array.Length<=1)
                return;
            int n= array.Length-1;
            MergeSort(array, 0, n);
        }
        private static void MergeSort(T[] array,int left, int right)
        {
            if (left >= right)
                return;
            int mid = left + (right - left) / 2;
            MergeSort(array, left, mid);
            MergeSort(array, mid + 1, right);
            Merge(array, left, mid, right);
        }
        private static void Merge(T[] array,int left,int mid,int right)
        {
            int nLeft = mid - left + 1;
            int nRight = right - mid ;
            T[] leftArray= new T[nLeft];
            T[] rightArray= new T[nRight];
            for(int i=0;i<nLeft;i++)
            {
                leftArray[i]=array[left+i];
            }
            for(int i=0;i<nRight;i++)
            {
                rightArray[i]=array[mid+i+1];
            }
            int iLeft = 0, iRight = 0, k = left;
            while(iLeft<nLeft&&iRight<nRight)
            {
                if (leftArray[iLeft].CompareTo(rightArray[iRight]) <= 0)
                {
                    array[k]=leftArray[iLeft];
                    iLeft++;
                }
                else
                {
                    array[k]=rightArray[iRight];
                    iRight++;
                }
                k++;
            }
            while(iLeft<nLeft)
            {
                array[k]=leftArray[iLeft];
                iLeft++;
                k++;
            }
            while(iRight<nRight)
            {
                array[k]=rightArray[iRight];
                iRight ++;
                k++;
            }
        }
    }
}
