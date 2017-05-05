[![Build Status](https://travis-ci.org/TheCageMan/seaweedfs-client.svg?branch=master)](https://travis-ci.org/TheCageMan/seaweedfs-client)
[![codecov](https://codecov.io/gh/TheCageMan/seaweedfs-client/branch/master/graph/badge.svg)](https://codecov.io/gh/TheCageMan/seaweedfs-client)
[![GitHub Release](http://img.shields.io/badge/Release-1.0.0.RELEASE-brightgreen.svg)](https://github.com/TheCageMan/seaweedfs-client-dotnet-core/releases/tag/1.0.0.RELEASE)
[![MIT license](http://img.shields.io/badge/license-MIT-blue.svg)](http://opensource.org/licenses/MIT)

# .Net 4.6 and .Net Core client for seaweed file system 
[This repository](https://github.com/TheCageMan/seaweedfs-client) is a port of the [lokra/seaweedfs-client](https://github.com/lokra/seaweedfs-client) GitHub code. This code has been refactored and extended to support .Net 4.6 and .Net Core Framework.

SeaweedFS is a simple and highly scalable distributed file system and started by implementing Facebook's Haystack design paper. SeaweedFS is currently growing, with more features on the way.

This .Net client encapsulates the SeaweedFS API functionality and provides a simple interface for easy integration.

## Quick Start

**Create a connection to Seaweed master server**
```shell
var master = new MasterConnection("localhost", 9333);
// Start manager and listen for changes
master.Start();
```

**Create file operation template and upload file**
```shell
var template = new OperationsTemplate(master.GetConnection());
template.SaveFileByStream("filename.doc", someFileStream);
```
