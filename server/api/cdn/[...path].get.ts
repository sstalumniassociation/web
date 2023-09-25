import type { R2Bucket } from '@cloudflare/workers-types'

export default defineCachedEventHandler(async (event) => {
  const bucket: R2Bucket = event.context.cloudflare.env.R2_SSTAA
  const file = await bucket.get(event.context.params!.path)
  if (!file) {
    throw createError({
      statusCode: 404,
      statusMessage: 'Not found',
    })
  }

  return sendStream(event, file.body as any)
}, {
  maxAge: 60,
})
