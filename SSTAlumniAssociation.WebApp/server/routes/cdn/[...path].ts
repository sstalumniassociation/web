import type { R2Bucket } from '@cloudflare/workers-types'

export default defineEventHandler(async (event) => {
  if (!event.context.cloudflare) {
    throw createError({
      statusCode: 404,
      statusMessage: 'Not found',
      message: 'Cloudflare R2 is not available in this environment.',
    })
  }

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
  setHeaders(event, Object.fromEntries(headers.entries()))
  setHeader(event, 'Cache-Control', `max-age=${60 * 60}`) // 1 hour cache

  return file.body
})
