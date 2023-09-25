import type { R2Bucket } from '@cloudflare/workers-types'

export default defineProtectedEventHandler(async (event) => {
  const bucket: R2Bucket = event.context.cloudflare.env.R2_SSTAA
  const file = await bucket.get(event.context.params!.path)

  if (!file) {
    throw createError({
      statusCode: 404,
      statusMessage: 'Not found',
    })
  }

  const headers = new Headers()
  file.writeHttpMetadata(headers as any)
  headers.set('etag', file.httpEtag)
  setResponseHeaders(event, Object.fromEntries(headers.entries()))

  return file.body
}, {
  cache: {
    maxAge: 60 * 60,
  },
})
