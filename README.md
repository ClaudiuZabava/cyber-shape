# `CYBER_SHAPE`

A low poly top-down shooter.

## For reviewers

Reports are in [`./Reports/`](./Reports/).

## For developers & testers

Required Unity version: 2022.1.20f1.

There are some `.blend` files in the project that might give you
errors if you don't have Blender installed, so we recommend you have
that too. (Though it shouldn't affect the DX too much if you don't
have it, as all the meshes are exported as FBX anyways).

Preferably, commit messages should conform to the following
convention, unless making an exception would make the message really
clearer:
- use English
- start with a capital letter
- use a verb in the imperative mood / present tense (such that the git
  log reads like a list of instructions)
- finish with a period.

For example, "Handle missing components on enemy instantiation." or
"Improve documentation of SomeFunction."

Please, try to keep the git log clean and informative. If, working on
the same feature branch, you make a commit that introduces a bug and
then fix that bug, it's better to amend the first commit than make a
second one. If you make multiple commits that concern the same thing,
you could squash them into a single one.

Only commit code that compiles! Do not commit conflicts!

It would be best if you could rebase your branches onto main before
sending a PR, as that would mean less conflicts to fix during the
merge and you ensure your changes work on the latest version of the
project.

As for the code style, what JetBrains' Rider recommends out of the box
is the preferred option. If you use Rider, whenever you see a squiggly
line under some text, check to see if it's a style recommendation!
