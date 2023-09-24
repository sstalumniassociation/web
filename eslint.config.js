import antfu from '@antfu/eslint-config'

export default antfu({
  stylistic: true, // enable stylistic formatting rules
  typescript: true,
  vue: true,
  jsonc: true,
  yaml: true,
}, {
  rules: {
    // https://github.com/antfu/eslint-config/pull/214
    'node/prefer-global/process': [
      'off',
      'never',
    ],
  },
})
