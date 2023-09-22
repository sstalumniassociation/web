import antfu from '@antfu/eslint-config'

export default antfu({
  stylistic: true, // enable stylistic formatting rules
  typescript: true,
  vue: true,
}, {
  rules: {
    // https://github.com/antfu/eslint-config/pull/214
    'n/prefer-global/process': [
      'off',
      'never',
    ],
  },
})
