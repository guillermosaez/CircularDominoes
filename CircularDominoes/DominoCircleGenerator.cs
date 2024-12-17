namespace CircularDominoes;

using System.Collections.Generic;

public class DominoCircleGenerator
{
    private readonly List<Domino> _dominoStones;

    public DominoCircleGenerator(List<Domino> dominoStones)
    {
        _dominoStones = dominoStones;
    }

    public Result<string> FindCircularChain()
    {
        if (_dominoStones.Count == 0)
        {
            var error = new Error("There aren't any dominoes to create a circle with");
            return Result<string>.Failure(error);
        }
        
        var chain = new List<Domino>();
        var used = new bool[_dominoStones.Count];
        var isDominoChainValid = Backtrack(chain, used, depth: 0);
        if (!isDominoChainValid)
        {
            var error = new Error("No circular domino chain is possible.");
            return Result<string>.Failure(error);
        }

        var chainString = string.Join(separator: " ", chain);
        return Result<string>.Success(chainString);
    }

    private bool Backtrack(List<Domino> chain, bool[] used, int depth)
    {
        if (depth == _dominoStones.Count)
        {
            // Check if the last domino connects back to the first
            return chain[0].Left == chain[^1].Right;
        }

        for (var i = 0; i < _dominoStones.Count; i++)
        {
            if (used[i]) continue;
            used[i] = true;

            // Try the domino as is
            chain.Add(_dominoStones[i]);
            if ((depth == 0 || chain[depth - 1].Right == chain[depth].Left) && Backtrack(chain, used, depth + 1))
            {
                return true;
            }

            chain.RemoveAt(chain.Count - 1);

            // Try the flipped version of the domino
            chain.Add(_dominoStones[i].Flip());
            if ((depth == 0 || chain[depth - 1].Right == chain[depth].Left) && Backtrack(chain, used, depth + 1))
            {
                return true;
            }

            chain.RemoveAt(chain.Count - 1);

            used[i] = false;
        }

        return false;
    }
}
