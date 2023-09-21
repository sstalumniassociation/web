import { getAuth } from 'firebase/auth'

export const $api = $fetch.create({
  async onRequest(context) {
    const token = await getAuth().currentUser?.getIdToken()
    if (token) {
      context.options.headers = {
        Authorization: `Bearer ${token}`,
      }
    }
  },
})
