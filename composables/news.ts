import { useQuery } from '@tanstack/vue-query'
import type { NewsArticle } from '~/shared/types'

const queryKeyFactory = {
  newsArticles: ['newsArticles'],
}

export function useNewsArticles() {
  const firebaseCurrentUser = useCurrentUser()
  return useQuery({
    queryKey: queryKeyFactory.newsArticles,
    queryFn: () => $api<NewsArticle[]>('/api/news'),
    enabled: computed(() => !!firebaseCurrentUser.value), // Only run when user exists
  })
}
