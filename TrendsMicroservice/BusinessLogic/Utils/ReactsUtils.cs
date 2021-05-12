using Entities;

namespace BusinessLogic.Utils
{
    public static class ReactsUtils
    {
        public static void UpdateDeltas(ref int deltaUpvotes, ref int deltaDownvotes, ReactType type, ReactType oldReactType)
        {
            switch (oldReactType)
            {
                case ReactType.Like:
                    ++deltaUpvotes;
                    if (oldReactType == ReactType.Dislike)
                    {
                        --deltaDownvotes;
                    }
                    break;
                case ReactType.Dislike:
                    ++deltaDownvotes;
                    if (oldReactType == ReactType.Like)
                    {
                        --deltaUpvotes;
                    }
                    break;
                case ReactType.None:
                    if (oldReactType == ReactType.Like)
                    {
                        --deltaUpvotes;
                    }
                    else if (oldReactType == ReactType.Dislike)
                    {
                        --deltaDownvotes;
                    }
                    break;
            }
        }
    }
}
