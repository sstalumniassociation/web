import type { AccessTokenProvider } from '@microsoft/kiota-abstractions'
import { AllowedHostsValidator, BaseBearerTokenAuthenticationProvider } from '@microsoft/kiota-abstractions'
import { FetchRequestAdapter } from '@microsoft/kiota-http-fetchlibrary'
import { getAuth } from 'firebase/auth'

import { createApiClient as createServiceAccountApiClient } from '~/api/service-account/apiClient'
import { createApiClient as createAdminApiClient } from '~/api/admin/apiClient'
import { createApiClient as createMemberApiClient } from '~/api/member/apiClient'

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

const serviceAccountAdapter = new FetchRequestAdapter(authProvider)
serviceAccountAdapter.baseUrl = config.public.api.serviceAccount

const adminAdapter = new FetchRequestAdapter(authProvider)
adminAdapter.baseUrl = config.public.api.admin

const memberAdapter = new FetchRequestAdapter(authProvider)
memberAdapter.baseUrl = config.public.api.member

export const $serviceAccountApiClient = createServiceAccountApiClient(serviceAccountAdapter)
export const $adminApiClient = createAdminApiClient(adminAdapter)
export const $memberApiClient = createMemberApiClient(memberAdapter)
