import { BrowserAgent } from '@newrelic/browser-agent/loaders/browser-agent'

export default defineNuxtPlugin(() => {
  const appConfig = useAppConfig()
  const { public: config } = useRuntimeConfig()

  const options = {
    init: { distributed_tracing: { enabled: true }, privacy: { cookies_enabled: true }, ajax: { deny_list: ['bam.eu01.nr-data.net'] } },
    info: { beacon: 'bam.eu01.nr-data.net', errorBeacon: 'bam.eu01.nr-data.net', licenseKey: 'NRJS-1b1f41c1d6baafe183c', applicationID: config.newRelic.applicationId, sa: 1 },
    loader_config: { accountID: '4337277', trustKey: '4337277', agentID: config.newRelic.agentId, licenseKey: 'NRJS-1b1f41c1d6baafe183c', applicationID: config.newRelic.applicationId },
  }

  const newrelic = new BrowserAgent(options)
  newrelic.setApplicationVersion(`${appConfig.buildInfo.version}-${appConfig.buildInfo.shortCommit}`)

  return {
    provide: {
      newrelic,
    },
  }
})
