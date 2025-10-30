# Changelog

All notable changes to this project will be documented in this file.

The format is based on [Keep a Changelog](https://keepachangelog.com/en/1.1.0/),
and this project adheres to [Semantic Versioning](https://semver.org/spec/v2.0.0.html).

## [Unreleased]

### Changed

- Upgrade Atata.Cli package to v4.0.0-beta.1.
- Upgrade Atata.Cli.Npm package to v4.0.0-beta.1.

## [2.4.0] - 2025-10-30

### Changed

- Enable nullable reference types.

## Fixed

- Fix absolute config path error for Windows.

## [2.3.0] - 2023-08-31

### Changed

- Set UFT-8 as the default encoding of `HtmlValidateCli`.

## [2.2.0] - 2022-10-04

### Changed

- Upgrade Atata.Cli package to v2.2.0.
- Upgrade Atata.Cli.Npm package to v2.2.0.

## [2.1.0] - 2022-07-21

### Changed

- Upgrade Atata.Cli package to v2.1.0.
- Upgrade Atata.Cli.Npm package to v2.1.0.
- Set `ResultValidationRules = CliCommandResultValidationRules.NoError;` in `HtmlValidateCli` constructor.

## [2.0.0] - 2022-05-10

### Changed

- Upgrade Atata.Cli package to v2.0.0.
- Upgrade Atata.Cli.Npm package to v2.0.0.

## [1.4.0] - 2022-03-25

### Changed

- Upgrade Atata.Cli package to v1.4.0.
- Upgrade Atata.Cli.Npm package to v1.4.0.

## [1.3.0] - 2021-07-23

### Changed

- Post process CLI output to correct encoding of "X" symbol character.
- Upgrade Atata.Cli package to v1.3.0.
- Upgrade Atata.Cli.Npm package to v1.3.0.

## [1.2.0] - 2021-07-21

### Changed

- Upgrade Atata.Cli package to v1.2.0.
- Upgrade Atata.Cli.Npm package to v1.2.0.

## Fixed

- Fix `HtmlValidateCli.GetInstalledVersion()` method to return `null` when the package is not installed.

## [1.1.0] - 2021-07-13

### Added

- Add Atata.Cli.Npm package v1.1.0.

### Changed

- Inherit `HtmlValidateCli` from `GlobalNpmPackageCli<HtmlValidateCli>` instead of `ProgramCli<HtmlValidateCli>`.

## [1.0.0] - 2021-06-25

Initial version release.

[Unreleased]: https://github.com/atata-framework/atata-cli-htmlvalidate/compare/v2.3.0...HEAD
[2.2.0]: https://github.com/atata-framework/atata-cli-htmlvalidate/compare/v2.2.0...v2.3.0
[2.2.0]: https://github.com/atata-framework/atata-cli-htmlvalidate/compare/v2.1.0...v2.2.0
[2.1.0]: https://github.com/atata-framework/atata-cli-htmlvalidate/compare/v2.0.0...v2.1.0
