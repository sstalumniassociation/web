import Git from 'simple-git'
import { defineNuxtModule } from '@nuxt/kit'
import { version } from '../package.json'
import type { BuildInfo } from '~/shared/types'

export default defineNuxtModule({
  meta: {
    name: 'web:build-env',
  },
  async setup(_options, nuxt) {
    const git = Git()

    const commit = await git.revparse(['HEAD'])
    const shortCommit = await git.revparse(['--short=7', 'HEAD'])

    const buildInfo: BuildInfo = {
      version,
      time: +Date.now(),
      commit,
      shortCommit,
    }

    nuxt.options.appConfig = nuxt.options.appConfig || {}
    nuxt.options.appConfig.buildInfo = buildInfo
  },
})
