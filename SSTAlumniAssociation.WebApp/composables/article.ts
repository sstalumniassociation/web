import { useMutation, useQuery, useQueryClient } from '@tanstack/vue-query'

const queryKeyFactory = {
  articles: ['articles'],
  article: (id: string) => ['articles', id],
}

export function useMemberArticles() {
  const firebaseCurrentUser = useCurrentUser()
  return useQuery({
    queryKey: queryKeyFactory.articles,
    queryFn: () => $memberApiClient.article.get(),
    enabled: computed(() => !!firebaseCurrentUser.value), // Only run when user exists
  })
}
