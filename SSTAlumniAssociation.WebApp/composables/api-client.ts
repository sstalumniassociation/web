import type { AccessTokenProvider } from '@microsoft/kiota-abstractions'
import { AllowedHostsValidator, BaseBearerTokenAuthenticationProvider } from '@microsoft/kiota-abstractions'
import { FetchRequestAdapter } from '@microsoft/kiota-http-fetchlibrary'
import { getAuth } from 'firebase/auth'
import { createPostsClient } from '~/api/postsClient'

const config = useRuntimeConfig()

class FirebaseAccessTokenProvider implements AccessTokenProvider {
  async getAuthorizationToken() {
    const token = await getAuth().currentUser?.getIdToken()
    return token ?? ''
  }

  getAllowedHostsValidator() {
    return new AllowedHostsValidator()
  }
}

const authProvider = new BaseBearerTokenAuthenticationProvider(new FirebaseAccessTokenProvider())
const adapter = new FetchRequestAdapter(authProvider)
adapter.baseUrl = config.public.api.url

export const $apiClient = createPostsClient(adapter)
