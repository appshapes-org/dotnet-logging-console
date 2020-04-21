#!/bin/bash

current_directory=$(pwd)
pushd $(find . -name $1)
dotnet tool restore; grep -oP -f $current_directory/stryker.project-reference.regex $1.csproj | xargs -I{} dotnet stryker --project-file={}
popd
