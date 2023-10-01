import { z } from 'zod'
import { initContract } from '@ts-rest/core'

const c = initContract()

const h3ErrorSchema = z.object({
  url: z.string(),
  statusCode: z.string(),
  statusMessage: z.string(),
  message: z.string(),
})

export const contract = c.router({
  getAdmission: {
    method: 'GET',
    path: '/api/admission/:key',
    summary: 'Get admission details by key',
    responses: {
      200: z.object({
        userId: z.string(),
        eventId: z.string(),
        admissionKey: z.string(),
        user: z.object({
          name: z.string(),
        }),
        event: z.object({
          id: z.string(),
          name: z.string(),
          description: z.string(),
          location: z.string(),
          badgeImage: z.string(),
          startDateTime: z.number(),
          endDateTime: z.number(),
        }),
      }),
      404: h3ErrorSchema,
    },
  },
})
