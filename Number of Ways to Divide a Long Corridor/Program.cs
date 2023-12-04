namespace Number_of_Ways_to_Divide_a_Long_Corridor
{
    internal class Program
    {
        static void Main(string[] args)
        {
            string corridor = "PPPPPSPPSPPSPPPSPPPPSPPPPSPPPPSPPSPPPSPSPPPSPSPPPSPSPPPSPSPPPPSPPPPSPPPSPPSPPPPSPSPPPPSPSPPPPSPSPPPSPPSPPPPSPSPSS";// SSPPSPS
            Solution solution = new Solution();
            var ans = solution.NumberOfWays_Dfs(corridor);
            Console.WriteLine(ans);
        }
    }

    public class Solution
    {
        public int NumberOfWays_Dfs(string corridor)
        {
            var cache = new Dictionary<(int, int), int>();
            int Dfs(int i, int seats)
            {
                var mod = 1000000007; 
                // base case
                // when we reach end of the corridor
                if (i == corridor.Length)
                {
                    // if we have found 2 seats when we are at the end, we surely can put a divider.
                    return seats == 2 ? 1 : 0;
                }

                var key = (i, seats);
                if (cache.ContainsKey(key)) return cache[key];

                // when seats count is 2, we are now in a place to put divider but based on the next character if it is 'S' or 'P'
                // if next is again 'S', surely we have to put divider as max 2 seats are allowed
                // if next is 'P', we can put a divider or can wait or can put divider after the 'P' if its a 'S' again


                // if there are 2 seats found
                int res = 0;
                if (seats == 2)
                {
                    // if the current char is 'S' again, surely we have to put divider and reset seat to 0, as we have found a 'S' now instead 0 set seats to 1.
                    if (corridor[i] == 'S')
                        res = Dfs(i + 1, 1);
                    else
                        // if the next is non 'S', we have to choice , 1. to put divider(reset seats to 0), 2. move to next 
                        res = (Dfs(i + 1, 0) + Dfs(i + 1, seats)) % mod;
                }
                else
                {
                    // if the seats count < 2, and next char is 'S' again, then ll increase seats count.
                    if (corridor[i] == 'S') seats += 1;
                    res = Dfs(i + 1, seats);
                }

                cache[key] = res;

                return res;
            }

            // start from first index and initially seats are 0
            return Dfs(0, 0); 
        }

        public int NumberOfWays(string corridor)
        {
            // get the index of the seats stored in a separate array
            // As we need 2 seats , the difference in distance bw 3rd seat - 2nd seat is the no of ways divider can be put
            // same waywe need to multiply all combinations
            var lst = new List<int>();
            for (int i = 0; i < corridor.Length; i++)
            {
                if (corridor[i] == 'S')
                {
                    lst.Add(i);
                }
            }

            // in case there are less than 2 seats or Odd no of seats we can not have an answer
            if (lst.Count < 1 || lst.Count % 2 == 1) return 0;

            // as the answer now can not be 0, so we initialize to 1 as multiplying other possibility with 1 is not harmful
            long res = 1;

            // start from 2nd seat
            int j = 1;
            while (j < lst.Count - 1)
            {
                var next = lst[j + 1];
                var current = lst[j];
                res = (res * (next - current)) % 1000000007;
                // why increment +2, when we are at the 2nd seat , next seat would be at 4th and next at 6th
                j += 2;
            }
            return (int)res;
        }
    }
}
